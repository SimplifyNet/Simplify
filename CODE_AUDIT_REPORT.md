# Аудит кода SimplifyNet — отчёт

**Дата:** 2026-06-22
**Ветка:** feature/review
**Область:** все библиотечные проекты `Simplify.*` (без тестов/примеров/интеграционных тестеров)
**Целевые фреймворки:** net10 / net9 / netstandard2.x / net48
**Зависимости:** актуальны (NHibernate 5.5.2, MailKit, Microsoft.Extensions 9.x)

## Резюме

Код аккуратный, тонкие обёртки над зрелыми библиотеками.

- **SQL-инъекций нет** — всё через LINQ-expression / `SqlConnectionStringBuilder`.
- **Грубых XXE-дыр нет** (нет `XmlDocument.LoadXml` / `XmlTextReader`).
- **Секреты нигде не логируются.**

Найдено несколько реальных дефектов (ниже по убыванию важности). Все находки подтверждены чтением исходников.

> **Статус (2026-06-22):** исправлены C1, C2, H1, H2, M2, M4, M8, M9 (пункты из раздела «Приоритеты к исправлению»). Логика ротации лога (M3) намеренно не менялась. Остальные пункты (H3, H4, M1, M5–M7, M10–M13, Low) — открыты.

---

## 🔴 Critical

### ✅ C1. Исключения в callback таймера роняют процесс — ИСПРАВЛЕНО
**Файлы:** `Simplify.Scheduler/SchedulerJobsHandler.cs:242-258`, `Simplify.WindowsServices/MultitaskServiceHandler.cs:274-303`

`OnCronTimerTick`/`OnStartWork` вызываются напрямую как `System.Threading.Timer` callback и кидают `ArgumentNullException`/`InvalidOperationException` (например, `CrontabProcessor == null`). Необработанное исключение из callback таймера летит на пул-поток без catch-фрейма → **аварийное завершение процесса**, минуя `OnException`.

**Фикс:** обернуть тело callback в try/catch и роутить в `TryRaiseOnExceptionEvent` / `OnException`; никогда не давать исключению покинуть callback таймера.

### ✅ C2. `throw;` внутри worker-задачи при отсутствии подписки на `OnException` — ИСПРАВЛЕНО
**Файлы:** `SchedulerJobsHandler.cs:292-296`, `MultitaskServiceHandler.cs:319-324`

Если обработчик `OnException` не подписан, исключение пере-выбрасывается внутри задачи (`Task.Factory.StartNew(...).Unwrap()`). При `Task.WaitAll` на остановке это `AggregateException` рушит graceful shutdown; иначе — unobserved exception.

**Фикс:** всегда логировать/роутить ошибку задания вместо `throw;`; в `WaitAll` терпимо относиться к faulted-задачам.

---

## 🟠 High

### ✅ H1. `CommonEqualityComparer`: `GetHashCode` несовместим с `Equals` + NRE — ИСПРАВЛЕНО
**Файл:** `Simplify.Repository/CommonEqualityComparer.cs:37`

`Equals` сравнивает по `ID`, а `GetHashCode(obj) => obj.GetHashCode()` возвращает reference-hash. В `Dictionary`/`HashSet`/`Distinct`/`GroupBy` равные по ID объекты попадают в разные корзины — компаратор деградирует до reference-equality. Плюс NRE при `obj == null` (хотя `Equals` null терпит).

**Фикс:**
```csharp
public int GetHashCode(object obj) =>
    obj is IIdentityObject id ? id.ID?.GetHashCode() ?? 0 : obj.GetHashCode();
```

### ✅ H2. Утечка/блокировка scope для basic-job — ИСПРАВЛЕНО
**Файлы:** `SchedulerJobsHandler.cs:316-340`, `MultitaskServiceHandler.cs:345-371` (+ `RunBasicJob(job).Wait()` :239)

Scope добавляется в `_workingBasicJobs` только **после** `await InvokeJobMethodAsync(...)`. Для бесконечного (server-style) задания await не завершается → scope не регистрируется для Dispose (утечка), а в WindowsServices `.Wait()` блокирует поток `OnStart` SCM → таймаут старта службы.

**Фикс:** регистрировать scope **до** await; запускать как фоновую задачу, не блокируя старт.

### ❌ H3. XML-парсинг без hardening (защита от XXE) — ОТКРЫТО
**Файлы:** `Simplify.Xml/XmlSerializer.cs`, `XmlExtensions.cs:98`

Прямых XXE нет, но библиотека не предоставляет hardened-входа (`DtdProcessing.Prohibit`, `XmlResolver=null`) и поощряет regex-манипуляции XML (`RemoveAllXmlNamespaces`). Defense-in-depth отсутствует.

**Фикс:** безопасный helper `XElement.Load(reader)` с запретом DTD; задокументировать «только доверенный XML». `RemoveAllXmlNamespaces` переписать через `XDocument` вместо regex.

### ❌ H4. Потеря исключений при `ParallelWhenAll` публикации событий — ОТКРЫТО
**Файл:** `Simplify.Bus.Impl/EventBusAsync\`1.cs:23-24`

`await Task.WhenAll(...)` пробрасывает только **первое** исключение; остальные упавшие хендлеры теряются.

**Фикс:** инспектировать `whenAllTask.Exception.InnerExceptions` и пробрасывать/агрегировать все ошибки.

---

## 🟡 Medium

| # | Статус | Файл | Проблема |
|---|--------|------|----------|
| M1 | ❌ открыто | `Simplify.System/WeakSingleton.cs:27-37` | Lock-free `TryGetTarget` гонится с `SetTarget` — `WeakReference<T>` не гарантирует потокобезопасность. |
| M2 | ✅ исправлено | `Simplify.Log/Logger.cs` (Write/WriteToFile) | Log injection: CR/LF из сообщения не экранируются → подделка строк лога. |
| M3 | ⏭️ намеренно пропущено | `Simplify.Log/Logger.cs:205-215` | TOCTOU при ротации: `Exists`→`Length`→`Delete`→`Append` без межпроцессной блокировки; ротация через `Delete` уничтожает лог. |
| M4 | ✅ исправлено (Logger) / ❌ открыто (FileHelper) | `Simplify.Log/Logger.cs` / `Simplify.IO/FileHelper.cs:124` | `Assembly.GetCallingAssembly().Location` пуст в single-file/AOT → файлы уходят в CWD. Лучше `AppContext.BaseDirectory`. |
| M5 | ❌ открыто | `Simplify.FluentNHibernate/SessionFactoryBuilderBase.cs:34` | Публичное свойство `ConnectionString` отдаёт строку с паролем. |
| M6 | ❌ открыто | `Simplify.FluentNHibernate/ConfigurationExtensions.cs` (Oracle/MySQL/PG) | Проверка null-пароля есть только для MsSql; остальные провайдеры молча строят connection без пароля. |
| M7 | ❌ открыто | `Simplify.FluentNHibernate/Interceptors/SqlStatementInterceptor.cs:25-40` | При `ShowSql` весь SQL пишется в `Console`/`Trace` — утечка query-shape/литералов. |
| M8 | ✅ исправлено | `Simplify.DI.Provider.Microsoft.Extensions.DependencyInjection/MicrosoftDependencyInjectionDIProvider.cs:60` | Дефолтный lifetime `Singleton` против `PerLifetimeScope` у остальных → captive dependencies. |
| M9 | ✅ исправлено | `Simplify.DI.Wcf/SimplifyInstanceProvider.cs` | На ошибке scope диспоузится, но остаётся прицеплен к `InstanceContext` → double-dispose в `ReleaseInstance`. |
| M10 | ❌ открыто | `Simplify.Templates/Template.cs:109` | Подстановка переменных без экранирования; значение с `{Other}` подменяется на след. проходе — инъекция в HTML/email. |
| M11 | ❌ открыто | `Simplify.Windows.Forms/ControlsValidator.cs`, `TextBoxesValidator.cs` | Повторный `EnableValidation()` подписывает хендлеры повторно (`+=` без `-=`) → утечка/мульти-валидация. |
| M12 | ❌ открыто | `Simplify.System/Extensions/BytesExtensions.cs:15` | Нечётная длина массива молча теряет последний байт; зависит от endianness. |
| M13 | ❌ открыто | `Simplify.Scheduler/SchedulerJobsHandler.cs` (`_jobs`) | `_jobs` (обычный `List<>`) читается/мутируется из разных потоков (Ctrl+C handler, Dispose, Stop) без синхронизации. |

---

## 🟢 Low

> Все пункты ниже — ❌ открыты (не исправлялись).

- **Mail TLS:** `EnableSsl` по умолчанию `false`, `StartTlsWhenAvailable` — уязвимо к downgrade-MITM (`Simplify.Mail/MailSender.cs:608,623`). Рекомендуется `StartTls` (required) по умолчанию или отказ от `Authenticate` при небезопасном соединении.
- **`AssemblyInfo.Title:125`** — fallback берёт имя **Simplify.System**, а не `_infoAssembly`.
- **`DoubleExtensions.AreSameAs`** — фиксированный эпсилон `1e-21` бессмыслен для больших чисел (нужно относительное/ULP-сравнение).
- **`DateTimeExtensions.TrimMilliseconds:17`** — теряет `DateTimeKind`.
- **`StringHelper.StripHtmlTags`** — `<.*?>` не является XSS-санитайзером (документировать как косметику).
- **`StringTable.Entry:17`** (Resources) — ленивая `??=` инициализация без локов (дубли-аллокации).
- **`MicrosoftDependencyInjectionDIProvider.Dispose()`** — no-op, не диспоузит `ServiceProvider` → утечка singletons.
- **`GC.Collect()` per task** в `FinalizeJob` (`SchedulerJobsHandler.cs:357`, `MultitaskServiceHandler.cs:401`) — опционально, но дорого под нагрузкой.
- **`FluentConfigurationExtension`** Export/UpdateSchema — `ISessionFactory` не диспоузится; rollback в catch без проверки `IsActive`.
- **`Simplify.Xml/XmlExtensions.cs:98` `RemoveAllXmlNamespaces`** — regex-манипуляция XML, ломается на CDATA/комментариях.
- **`FileHelper.IsFileLockedForRead`** — любой `IOException` трактуется как «locked», маскируя реальные ошибки.

---

## Проверено и признано корректным

- IDisposable в UnitOfWork / TransactUnitOfWork (FNH и EF) реализованы по паттерну верно.
- Lifetime транзакций: Commit/Rollback корректно null-чекают и обнуляют `_transaction`.
- Цепочка behavior'ов шины (`BehaviorChainWrapper`, `WrapUp`) — порядок и терминальный handler верны.
- `Conveyor`/`AsyncConveyor` — исключения пробрасываются корректно, `ConfigureAwait(false)` присутствует.
- Нет `async void` (кроме обоснованного Ctrl+C handler), нет `.Result`/`.Wait` deadlock-паттернов (кроме H2).
- Lifetime scopes провайдеров DI (DryIoc, Autofac, SimpleInjector, CastleWindsor) диспоузятся один раз.
- `CrontabProcessor` корректно лочит доступ к `NextOccurrences`.

---

## Приоритеты к исправлению

1. ✅ **C1 + C2** (Scheduler/WindowsServices) — реальный краш процесса/службы, чинить в первую очередь.
2. ✅ **H1** (`CommonEqualityComparer`) — тихие баги в любой hash-коллекции, тривиальный фикс.
3. ✅ **H2** — служба с непрерывным заданием не стартует / течёт.
4. ✅ **M2/M4** (Logger) и ✅ **M8/M9** (DI lifetime) — следующая волна. *(M3 — ротация лога — намеренно пропущена.)*

**Сделано:** C1, C2, H1, H2, M2, M4 (Logger), M8, M9. Собрано и протестировано (Microsoft DI provider — 41 тест зелёный).
**Осталось:** H3, H4, M1, M4 (FileHelper), M5–M7, M10–M13 и все Low.

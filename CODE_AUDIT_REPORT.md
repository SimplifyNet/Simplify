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

> **Статус (2026-06-22):** исправлены C1, C2, H1, H2, M2, M4, M8, M9 (пункты из раздела «Приоритеты к исправлению»). Логика ротации лога (M3) намеренно не менялась.
>
> **Статус (2026-06-23):** дополнительно исправлены H4, M1, M4 (FileHelper), M11, M12, M13 и Low-пункты (AssemblyInfo.Title, DateTimeExtensions Kind, StringTable-лок, MicrosoftDI.Dispose, FluentConfigurationExtension, документация StripHtmlTags). По решению владельца **намеренно не трогались** пункты, меняющие публичный API / дефолты безопасности или ломающие существующие тесты: H3 (XML hardening + переписывание RemoveAllXmlNamespaces), M5, M6, M7, M10 (Template), Mail TLS-дефолт, DoubleExtensions (относительный epsilon ломает существующий тест), а также «врождённые» Low (IsFileLockedForRead, GC.Collect — opt-in).

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

### ✅ H4. Потеря исключений при `ParallelWhenAll` публикации событий — ИСПРАВЛЕНО
**Файл:** `Simplify.Bus.Impl/EventBusAsync\`1.cs:23-24`

`await Task.WhenAll(...)` пробрасывал только **первое** исключение; остальные упавшие хендлеры терялись.

**Фикс:** при >1 упавшем хендлере пробрасывается `whenAllTask.Exception` (полный `AggregateException`); при одном — оригинальное исключение (сохранён стек).

---

## 🟡 Medium

| # | Статус | Файл | Проблема |
|---|--------|------|----------|
| M1 | ✅ исправлено | `Simplify.System/WeakSingleton.cs:27-37` | Lock-free `TryGetTarget` гонился с `SetTarget`. Теперь чтение и запись под одним локом; поле `_ref` сделано `readonly`. |
| M2 | ✅ исправлено | `Simplify.Log/Logger.cs` (Write/WriteToFile) | Log injection: CR/LF из сообщения не экранируются → подделка строк лога. |
| M3 | ⏭️ намеренно пропущено | `Simplify.Log/Logger.cs:205-215` | TOCTOU при ротации: `Exists`→`Length`→`Delete`→`Append` без межпроцессной блокировки; ротация через `Delete` уничтожает лог. |
| M4 | ✅ исправлено (Logger) / ✅ исправлено (FileHelper) | `Simplify.Log/Logger.cs` / `Simplify.IO/FileHelper.cs:124` | `GenerateFullName` теперь использует `AppContext.BaseDirectory` (корректно в single-file/AOT). |
| M5 | ❌ открыто | `Simplify.FluentNHibernate/SessionFactoryBuilderBase.cs:34` | Публичное свойство `ConnectionString` отдаёт строку с паролем. |
| M6 | ❌ открыто | `Simplify.FluentNHibernate/ConfigurationExtensions.cs` (Oracle/MySQL/PG) | Проверка null-пароля есть только для MsSql; остальные провайдеры молча строят connection без пароля. |
| M7 | ❌ открыто | `Simplify.FluentNHibernate/Interceptors/SqlStatementInterceptor.cs:25-40` | При `ShowSql` весь SQL пишется в `Console`/`Trace` — утечка query-shape/литералов. |
| M8 | ✅ исправлено | `Simplify.DI.Provider.Microsoft.Extensions.DependencyInjection/MicrosoftDependencyInjectionDIProvider.cs:60` | Дефолтный lifetime `Singleton` против `PerLifetimeScope` у остальных → captive dependencies. |
| M9 | ✅ исправлено | `Simplify.DI.Wcf/SimplifyInstanceProvider.cs` | На ошибке scope диспоузится, но остаётся прицеплен к `InstanceContext` → double-dispose в `ReleaseInstance`. |
| M10 | ❌ открыто | `Simplify.Templates/Template.cs:109` | Подстановка переменных без экранирования; значение с `{Other}` подменяется на след. проходе — инъекция в HTML/email. |
| M11 | ✅ исправлено | `Simplify.Windows.Forms/ControlsValidator.cs`, `TextBoxesValidator.cs` | Подписка хендлеров теперь однократная (флаг `_handlersAttached`); повторный `EnableValidation()` только перевалидирует. |
| M12 | ✅ исправлено | `Simplify.System/Extensions/BytesExtensions.cs:15` | Добавлены null-чек и `ArgumentException` при нечётной длине вместо тихой потери последнего байта. |
| M13 | ✅ исправлено | `Simplify.Scheduler/SchedulerJobsHandler.cs` (`_jobs`) | Доступ к `_jobs` синхронизирован (`lock (_jobs)`): добавление и снимок перед итерацией в Start/Stop/Dispose. |

---

## 🟢 Low

- ❌ **Mail TLS:** `EnableSsl` по умолчанию `false`, `StartTlsWhenAvailable` — уязвимо к downgrade-MITM (`Simplify.Mail/MailSender.cs:608,623`). Рекомендуется `StartTls` (required) по умолчанию или отказ от `Authenticate` при небезопасном соединении. *(намеренно не трогали — смена дефолта ломает клиентов к non-TLS SMTP).*
- ✅ **`AssemblyInfo.Title:125`** — fallback теперь использует `_infoAssembly.GetName().Name` вместо `Assembly.GetExecutingAssembly()` (и AOT-безопасно, без `Location`).
- ❌ **`DoubleExtensions.AreSameAs`** — фиксированный эпсилон `1e-21` бессмыслен для больших чисел (нужно относительное/ULP-сравнение). *(намеренно не трогали — относительное сравнение ломает существующий тест `Double_CompareTwoDoubles_ComparedCorrectly`).*
- ✅ **`DateTimeExtensions.TrimMilliseconds:17`** — теперь сохраняет `DateTimeKind`.
- ✅ **`StringHelper.StripHtmlTags`** — добавлена документация: косметика, не XSS-санитайзер.
- ✅ **`StringTable.Entry:17`** (Resources) — ленивая инициализация под локом.
- ✅ **`MicrosoftDependencyInjectionDIProvider.Dispose()`** — теперь диспоузит `ServiceProvider`.
- ❌ **`GC.Collect()` per task** в `FinalizeJob` (`SchedulerJobsHandler.cs:357`, `MultitaskServiceHandler.cs:401`) — уже opt-in (гейт `CleanupOnTaskFinish`), оставлено как есть.
- ✅ **`FluentConfigurationExtension`** Export/UpdateSchema — `ISessionFactory` теперь диспоузится (`using`); rollback под проверкой `tx.IsActive`.
- ❌ **`Simplify.Xml/XmlExtensions.cs:98` `RemoveAllXmlNamespaces`** — regex-манипуляция XML, ломается на CDATA/комментариях. *(намеренно не трогали — переписывание меняет вывод и ломает существующий тест).*
- ❌ **`FileHelper.IsFileLockedForRead`** — любой `IOException` трактуется как «locked». Это и есть стандартный паттерн проверки; оставлено как есть.

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

**Сделано:** C1, C2, H1, H2, H4, M1, M2, M4, M8, M9, M11, M12, M13 + Low (AssemblyInfo.Title, DateTimeExtensions Kind, StringTable-лок, MicrosoftDI.Dispose, FluentConfigurationExtension, StripHtmlTags doc). Собрано (`Simplify.NetCore.slnx`, 0 ошибок) и протестировано (System/Bus/IO/Resources/String/Microsoft DI — 89 тестов зелёных).
**Осталось намеренно (меняют публичный API / дефолты безопасности / ломают тесты):** H3, M3 (ротация лога), M5, M6, M7, M10 (Template), Mail TLS-дефолт, DoubleExtensions epsilon, RemoveAllXmlNamespaces, IsFileLockedForRead, GC.Collect (opt-in).

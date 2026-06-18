# Simplify Library Review — Issues / Tasks

**Review Date:** 2026-06-18  
**Source file: full details:** See `review-detailed.md` (not committed) or chat history  
**Tracking format:** ID, priority, status, files affected, short description

## Status Key
- 🔴 = Critical (fix ASAP) / 🟡 = Medium / 🔵 = Low

---

## HIGH PRIORITY (Critical)

| ID | Issue | Files Affected | Status |
|----|-------|---------------|--------|
| C-01 | WeakSingleton.Instance race condition | `Simplify.System/WeakSingleton.cs:28-37` |  |
| C-02 | Race conditions on ??= lazy init (multiple singletons) | `ApplicationEnvironment.cs:46`, `TimeProvider.cs:21`, `AssemblyInfo.cs:30` |  |
| C-03 | Static AntiSpamPool memory leak — infinite growth | `MailSender.cs:654-676` |  |
| C-04 | Semaphore deadlock risk on Dispose | `MailSender.cs:571-596` |  |
| C-05 | ObjectConverter base class leaves ConvertFunc null at construction | `Simplify.System/Converters/ObjectConverter.cs:24`, `Simplify.Xml/Converters/ObjectConverter.cs:24` |  |
| C-06 | Duplicate converter classes across projects (DRY violation) | `ObjectConverter`, `ChainedObjectConverter`, `IObjectConverter` in both System and Xml |  |
| C-07 | Missing null-guards in Pipeline constructors (no ArgumentNullException.ThrowIfNull) | `Pipeline.cs:22`, `ResultingPipeline:T`:23, `ValidationPipeline:T:20`, `PipelineProcessor:T:23-24`, `ValidationPipelineProcessor:T:24` |  |
| C-08 | Exception swallowing in FileHelper.IsFileLockedForRead | `FileHelper.cs:58-64` |  |
| C-09 | Stream resource leak — Close() instead of Dispose() | `FileHelper.cs:68` |  |
| C-10 | Config parsing without validation (int/bool/Enum Parse crashes on bad input) | `ConfigurationBasedMailSenderSettings.cs`, `ConfigurationBasedLoggerSettings.cs` |  |

## MEDIUM PRIORITY

| ID | Issue | Files Affected | Status |
|----|-------|---------------|--------|
| M-01 | AppDomain.ProcessExit unassigned event param (dead code) | `ApplicationEnvironment.cs:13` |  |
| M-02 | ResourcesStringTable.GetString returns null for missing key silently | `ResourcesStringTable.cs:47` |  |
| M-03 | Logger.GetInnerExceptionData returns null, string-interpolated as "null" | `Logger.cs:183`, `Logger.cs:194` |  |
| M-04 | Assembly.GetCallingAssembly().Location can be null (merged assemblies) | `Logger.cs:200` |  |
| M-05 | ArgumentNullException without parameter name | `StringTable.cs:18` |  |
| M-06 | StringHelper.ParseMobilePhone missing null check on phone param | `StringHelper.cs:17-20` |  |
| M-07 | ResourcesStringTable accepts baseName=null without validation | `ResourcesStringTable.cs:20,33` |  |
| M-08 | Obsolete types pollute public API (never removed after years) | `Simplify.Pipelines/*` |  |

## LOW PRIORITY

| ID | Issue | Files Affected | Status |
|----|-------|---------------|--------|
| L-01 | Cross-platform path separator (/ instead of Path.Combine) | `FileHelper.cs:130` |  |
| L-02 | Dead code — Regex null check never triggers | `XmlExtensions.cs:108` |  |

## ARCHITECTURAL NOTES (not tasks per se)

- Static mutable state throughout project (`ApplicationEnvironment.CurrentTime`, `TimeProvider.Current`, `Logger.FileSystem`, `FileHelper.FileSystem`) — needs DI or thread-safe patterns
- Nullable reference type annotations missing from public interfaces (`IMailSender`, `IResourcesStringTable`, etc.)

---

## Progress log

| Date | Issue(s) addressed | Status update |
|----|--------|------|
| 2026-06-18 | Review completed, issues catalogued | All issues marked "pending" |


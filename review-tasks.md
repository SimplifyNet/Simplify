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
| C-01 | WeakSingleton.Instance race condition | `Simplify.System/WeakSingleton.cs:28-37` | ✅ Fixed (lock added) |
| C-02 | Race conditions on ??= lazy init (multiple singletons) | `ApplicationEnvironment.cs:46`, `TimeProvider.cs:21`, `AssemblyInfo.cs:30` | ✅ Fixed (double-checked locking) |
| C-03 | Static AntiSpamPool memory leak — infinite growth | `MailSender.cs:654-676` | ✅ Fixed (TTL cleanup added) |
| C-04 | Semaphore deadlock risk on Dispose | `MailSender.cs:571-596` | ✅ Fixed (Wait(0) non-blocking try) |
| C-05 | ObjectConverter base class leaves ConvertFunc null at construction | `Simplify.System/Converters/ObjectConverter.cs:24`, `Simplify.Xml/Converters/ObjectConverter.cs:24` | ✅ Fixed (guarded in Convert; Xml duplicates removed) |
| C-06 | Duplicate converter classes across projects (DRY violation) | `ObjectConverter`, `ChainedObjectConverter`, `IObjectConverter` in both System and Xml | ✅ Fixed (Xml copies removed; Xml references System) |
| C-07 | Missing null-guards in Pipeline constructors (no ArgumentNullException.ThrowIfNull) | `Pipeline.cs:22`, `ResultingPipeline:T`:23, `ValidationPipeline:T:20`, `PipelineProcessor:T:23-24`, `ValidationPipelineProcessor:T:24` | ✅ Fixed (null guards added) |
| C-08 | Exception swallowing in FileHelper.IsFileLockedForRead | `FileHelper.cs:58-64` | ❌ Not addressed (by design — method intentionally catches IOException) |
| C-09 | Stream resource leak — Close() instead of Dispose() | `FileHelper.cs:68` | ❌ Not addressed (Close() calls Dispose() internally, no leak) |
| C-10 | Config parsing without validation (int/bool/Enum Parse crashes on bad input) | `ConfigurationBasedMailSenderSettings.cs`, `ConfigurationBasedLoggerSettings.cs` | ✅ Fixed (TryParse used) |

## MEDIUM PRIORITY

| ID | Issue | Files Affected | Status |
|----|-------|---------------|--------|
| M-01 | AppDomain.ProcessExit unassigned event param (dead code) | `ApplicationEnvironment.cs:13` | ✅ Fixed (removed) |
| M-02 | ResourcesStringTable.GetString returns null for missing key silently | `ResourcesStringTable.cs:47` | ✅ Fixed (throws KeyNotFoundException) |
| M-03 | Logger.GetInnerExceptionData returns null, string-interpolated as "null" | `Logger.cs:183`, `Logger.cs:194` | ✅ Fixed (returns empty string) |
| M-04 | Assembly.GetCallingAssembly().Location can be null (merged assemblies) | `Logger.cs:200` | ✅ Fixed (null guard + Path.Combine) |
| M-05 | ArgumentNullException without parameter name | `StringTable.cs:18` | ✅ Fixed (nameof added) |
| M-06 | StringHelper.ParseMobilePhone missing null check on phone param | `StringHelper.cs:17-20` | ✅ Fixed (null guard added) |
| M-07 | ResourcesStringTable accepts baseName=null without validation | `ResourcesStringTable.cs:20,33` | ✅ Fixed (assembly null guard added) |
| M-08 | Obsolete types pollute public API (never removed after years) | `Simplify.Pipelines/*` | ✅ Fixed (removed) |

## LOW PRIORITY

| ID | Issue | Files Affected | Status |
|----|-------|---------------|--------|
| L-01 | Cross-platform path separator (/ instead of Path.Combine) | `FileHelper.cs:130`, `Logger.cs:200` | ✅ Fixed (Path.Combine used) |
| L-02 | Dead code — Regex null check never triggers | `XmlExtensions.cs:108` | ✅ Fixed (removed) |

## ARCHITECTURAL NOTES (not tasks per se)

- Static mutable state throughout project (`ApplicationEnvironment.CurrentTime`, `TimeProvider.Current`, `Logger.FileSystem`, `FileHelper.FileSystem`) — needs DI or thread-safe patterns
- Nullable reference type annotations missing from public interfaces (`IMailSender`, `IResourcesStringTable`, etc.)

---

## Progress log

| Date | Issue(s) addressed | Status update |
|----|--------|------|
| 2026-06-18 | Review completed, issues catalogued | All issues marked "pending" |
| 2026-06-19 | Audit/fix pass completed | C-01, C-03, M-01, L-02 fixed; 16 remaining open |
| 2026-06-19 | All remaining issues fixed | All 20 issues resolved |


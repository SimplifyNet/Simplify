# Changelog

## [2.2.0] - 2026-07-11

### Added

- BCC recipient support on all multi-recipient overloads
- `IAntiSpamPool` / `AntiSpamPool` extracted from `MailSender` with O(m) queue-based expiry and configurable max-items eviction (default 10 000) to prevent OOM
- `MailSender.DefaultAntiSpamPool` static property to swap the pool implementation without constructor injection
- Port validation in `MailSenderSettings` (throws `ArgumentOutOfRangeException` on out-of-range values)

### Fixed

- Double-checked locking in `SmtpClient` getter replaced with `Lazy<SmtpClient>(ExecutionAndPublication)` for correct thread-safety on all architectures
- `Dispose` no longer disposes `SemaphoreSlim` while another thread may hold it; blocks until in-flight send completes, then disposes cleanly
- `CheckAntiSpamPool` redundant `ContainsKey` check removed
- Integration test assertions added for construction and port default

### Performance

- `SendSeparately` / `SendSeparatelyAsync` now holds the SMTP lock for the entire batch instead of per-recipient acquire/release
- Anti-spam pool expiry uses queue-based O(m) scan instead of full O(n) dictionary enumeration

### Added

- .NET 10 support

### Security

- SMTP authentication no longer falls back to a plain-text connection when SSL is not explicitly enabled: STARTTLS is now negotiated opportunistically (`StartTlsWhenAvailable`) so credentials are encrypted whenever the server supports it

### Fixed

- `Dispose` is now idempotent and thread-safe (guarded with `Interlocked`)
- Anti-spam pool now uses `DateTime.UtcNow` to avoid mis-expiration on local clock/DST changes
- `Dispose` no longer blocks indefinitely on `_smtpLock.Wait()` when another thread holds the semaphore; uses non-blocking `Wait(0)` instead
- `ConfigurationBasedMailSenderSettings` config parsing now uses `TryParse` instead of `Parse` to avoid crashing on invalid configuration values

### Dependencies

- MailKit bump to 4.17
- Microsoft.Extensions.Configuration bump to 10.0.9

## [2.0.1] - 2026-04-24

### Dependencies

- MailKit bump to 4.16

## [2.0] - 2025-11-20

### Breaking change

- Switched from System.Net.Mail to MailKit

### Removed

- System.Configuration.ConfigurationManager configuration support

### Dependencies

- Microsoft.Extensions.Configuration bump to 9.0.11

## [1.6.0] - 2025-06-15

### Removed

- .NET 6 support

### Added

- .NET 9 support

### Dependencies

- Microsoft.Extensions.Configuration bump to 9.0.6
- System.Configuration.ConfigurationManager bump to 9.0.6

## [1.5.5] - 2022-09-08

### Added

- .NET 6 support
- .NET Standard 2.1 support

### Dependencies

- Microsoft.Extensions.Configuration bump to 3.1.32

## [1.5.4] - 2022-09-08

### Fixed

- Collection was modified; enumeration operation may not execute.

## [1.5.3] - 2022-07-21

### Fixed

- Anti-spam pool made static (#416)

## [1.5.2] - 2022-04-29

### Fixed

- SmtpClient Disposing, MailSender is disposable

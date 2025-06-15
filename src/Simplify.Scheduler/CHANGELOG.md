# Changelog

## [1.6.0] - 2025-06-15

### Removed

- .NET 6.0 support

### Added

- .NET 8.0 support
- .NET 9.0 support

### Dependencies

- ncrontab bump to 3.3.3
- Simplify.DI bump to 4.2.11
- Microsoft.Extensions.Configuration bump to 3.1.32

## [1.5.0] - 2024-06-02

### Added

- Logging crontab parse errors to SchedulerJobsHandler.OnException (#490)
- MultitaskScheduler.StartAsync method (To use scheduler asynchronously from `async Task Main` method) (#491)

## [1.4.0] - 2024-01-09

### Added

- Null checks
- Switch to better exceptions

## [1.3.1] - 2023-08-24

### Added

- .NET 6.0 support
- .NET Standard 2.1 support
- .NET Framework 4.8 support

### Dependencies

- Simplify.DI bump to 4.2.10
- Simplify.System bump to 1.6.2
- Microsoft.Extensions.Configuration bump to 9.0.6

## [1.3] - 2021-11-18

### Added

- Async run method support (#326)

## [1.2] - 2021-05-25

### Added

- Ability to create customized jobs (initialized from code and not from settings) (#293)

## [1.1.2] - 2021-05-24

### Fixes

- App name retrieved is incorrect (#292)

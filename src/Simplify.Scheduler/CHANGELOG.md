# Changelog

## [1.7.0] - 2026-06-25

### Fixed

- Crontab job timers were never stopped/disposed on shutdown, leaking timers that kept firing during and after stop; all job timers are now stopped before waiting for running tasks and on dispose
- Data race in `CrontabProcessor` between `IsMatching` and `CalculateNextOccurrences` over the shared occurrences list; access is now synchronized
- Lifetime scope leak in basic job execution when resolving or invoking the job throws; the scope is now disposed on failure
- `MultitaskScheduler` now unsubscribes from the static `Console.CancelKeyPress` event on dispose, preventing the disposed instance from being rooted and from receiving Ctrl+C after disposal
- Exceptions thrown from the crontab timer callback (`OnCronTimerTick`/`OnStartWork`) escaped to a thread-pool thread and terminated the process; they are now routed to the `OnException` event
- Unhandled job exceptions were rethrown from inside the worker task, surfacing as an `AggregateException` from `Task.WaitAll` and aborting graceful shutdown; they are no longer rethrown
- Basic (long-running) job lifetime scope was registered for disposal only after the job method returned, leaking the scope for never-completing jobs and blocking startup; the scope is now registered before the job runs and the job is executed without blocking startup

### Added

- .NET 10 support

### Dependencies

- Simplify.DI bump to 4.3
- Simplify.System bump to 1.6.3
- Microsoft.Extensions.Configuration bump to 10.0.9

## [1.6.1] - 2025-10-10

### Dependencies

- ncrontab bump to 3.4
- Microsoft.Extensions.Configuration bump to 9.0.9

## [1.6.0] - 2025-06-15

### Removed

- .NET 6.0 support

### Added

- .NET 8.0 support
- .NET 9.0 support

### Dependencies

- ncrontab bump to 3.3.3
- Simplify.DI bump to 4.2.11
- Microsoft.Extensions.Configuration bump to 9.0.6

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
- Microsoft.Extensions.Configuration bump to 3.1.32

## [1.3] - 2021-11-18

### Added

- Async run method support (#326)

## [1.2] - 2021-05-25

### Added

- Ability to create customized jobs (initialized from code and not from settings) (#293)

## [1.1.2] - 2021-05-24

### Fixes

- App name retrieved is incorrect (#292)

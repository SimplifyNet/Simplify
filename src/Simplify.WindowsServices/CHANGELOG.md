# Changelog

## [2.16] - 2026-06-25

### Fixed

- Crontab job timers were never stopped/disposed on service stop/dispose, leaking timers that kept firing during and after shutdown; all job timers are now stopped before waiting for running tasks and on dispose
- Data race in `CrontabProcessor` between `IsMatching` and `CalculateNextOccurrences` over the shared occurrences list; access is now synchronized
- Lifetime scope leak in basic job execution when resolving or invoking the job throws; the scope is now disposed on failure
- Exceptions thrown from the crontab timer callback (`OnCronTimerTick`/`OnStartWork`) escaped to a thread-pool thread and terminated the service process; they are now routed to the `OnException` event
- Unhandled job exceptions were rethrown from inside the worker task, surfacing as an `AggregateException` from `Task.WaitAll` and aborting graceful shutdown; they are no longer rethrown
- Basic (long-running) job blocked `OnStart` (causing Service Control Manager start timeouts) and its lifetime scope was registered for disposal only after the job method returned, leaking the scope; the job now runs as a background task and the scope is registered before it runs

#### Dependencies

- Simplify.DI bump to 4.3
- Simplify.System bump to 1.6.3
- Microsoft.Extensions.Configuration bump to 10.0.9

## [2.15.1] - 2025-10-10

### Dependencies

- ncrontab bump to 3.4
- Microsoft.Extensions.Configuration bump to 9.0.6

## [2.15.0] - 2025-06-15

### Dependencies

- ncrontab bump to 3.3.3
- Simplify.DI bump to 4.2.11
- Microsoft.Extensions.Configuration bump to 9.0.6

## [2.14.1] - 2023-08-24

### Removed

- .NET Framework 4.6.2 support

### Added

- .NET Framework 4.8 support

### Dependencies

- Simplify.DI bump to 4.2.10
- Simplify.System bump to 1.6.2
- Microsoft.Extensions.Configuration bump to 3.1.32

## [2.14] - 2021-11-18

### Added

- Async run method support (#327)

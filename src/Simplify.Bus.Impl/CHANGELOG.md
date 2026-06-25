# Changelog

## [0.1.3] - 2026-06-25

### Added

- Explicit .NET 10 Support

### Fixed

- `EventBusAsync<TEvent>` with the `ParallelWhenAll` strategy rethrew only the first handler exception (the default `await Task.WhenAll` behavior), silently losing the rest; when more than one handler fails the full `AggregateException` is now thrown

### Dependencies

- Simplify.DI bump to 4.3

## [0.1.2] - 2025-10-10

### Dependencies

- Simplify.DI bump to 4.2.11

## [0.1.1] - 2022-06-21

### Added

- Simplify.Bus RegisterBehaviorsList registers Ilist instead of IReadOnlyList (#405)


## [0.1.0] - 2022-05-24

### Added

- Initial release

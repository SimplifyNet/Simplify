# Changelog

## [1.1.0] - 2026-06-19

### Added

- .NET 10 support
- `ResourcesStringTable.GetRequiredString`, throws `KeyNotFoundException` if key is not found

### Fixed

- `ResourcesStringTable` now throws a descriptive `InvalidOperationException` instead of a `NullReferenceException` when the entry assembly cannot be resolved (e.g. in unmanaged/host scenarios)
- `ResourcesStringTable(Assembly, ...)` constructor now validates the assembly parameter with `ArgumentNullException`
- `StringTable.Entry` setter now uses `nameof(value)` in `ArgumentNullException` instead of an empty parameter name

## [1.0.3] - 2023-08-01

### Removed

- .NET 5 support
- .NET 4.5.2 support

### Added

- .NET 6 support
- .NET Standard 2.1 support
- .NET 4.8 support

## [1.0.2] - 2021-08-24

### Added

- .NET 5 explicit support

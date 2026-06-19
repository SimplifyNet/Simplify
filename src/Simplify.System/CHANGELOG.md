# Changelog

## [1.6.3] - 2026-06-19

### Added

- .NET 10 support

### Fixed

- `WeakSingleton<T>` was not thread-safe and could create multiple instances under concurrent access; instance creation is now synchronized with double-checked locking

## [1.6.2] - 2023-08-02

### Removed

- .NET 5 support
- .NET 4.5.2 support
- .NET Core 3.1 support

### Added

- .NET 6 support
- .NET Standard 2.1 support
- .NET 4.8 support

## [1.6.1] - 2021-04-21

### Added

- .NET 5 explicit support

### Fixed

- Assembly CodeBase deprecation fix

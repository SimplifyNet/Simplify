# Changelog

## [1.6.3] - 2026-06-19

### Added

- .NET 10 support

### Fixed

- `WeakSingleton<T>` was not fully thread-safe: the lock-free `TryGetTarget` fast path could race with target re-creation (`WeakReference<T>` instance members are not guaranteed thread-safe); reads and writes are now fully synchronized under a single lock
- `ApplicationEnvironment.Name`, `TimeProvider.Current`, `AssemblyInfo.Entry` lazy initialization was not thread-safe (`??=`) and could return different instances under concurrent first access; initialization is now synchronized with double-checked locking
- `ObjectConverter<T>` parameterless protected constructor left `ConvertFunc` null; `Convert` now guards against null with a descriptive exception
- `BytesExtensions.GetString` silently dropped the last byte of an odd-length array; it now throws `ArgumentNullException`/`ArgumentException` instead of returning truncated data
- `AssemblyInfo.Title` fallback resolved the name of the executing assembly (`Simplify.System`) instead of the wrapped assembly; it now returns the wrapped assembly name and no longer relies on `Assembly.Location` (empty in single-file/AOT)
- `DateTimeExtensions.TrimMilliseconds` reset `DateTimeKind` to `Unspecified`; it now preserves the original `Utc`/`Local`/`Unspecified` kind

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

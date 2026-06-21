# Changelog

## [1.5.1] - 2026-06-19

### Added

- .NET 10 support

### Fixed

- `FileHelper.IsFileLockedForRead` and `GetLastLineOfFile` now use the configurable `IFileSystem` abstraction instead of direct file access, so they honor a custom/mocked file system
- `FileHelper.GenerateFullName` now uses `Path.Combine` instead of a hardcoded `"/"` path separator for cross-platform compatibility

## [1.5.0] - 2025-06-15

### Removed

- .NET 6 support

### Added

- .NET 9 support

### Dependencies

- System.IO.Abstractions bump to 22.0.14

## [1.4.0] - 2023-08-26

### Removed

- .NET Framework 4.6.2 support

### Added

- .NET Standard 2.1 support
- .NET Framework 4.8 support

### Dependencies

- System.IO.Abstractions bump to 19.2.67

## [1.3.0] - 2022-11-30

### Dependencies

- System.IO.Abstractions bump to 18.0.1 (PR#467)

## [1.2.5] - 2022-05-02

### Dependencies

- System.IO.Abstractions bump to 17.0.3 (PR#380)

## [1.2.4] - 2022-01-03

### Dependencies

- System.IO.Abstractions bump to 16.0.1 (PR#331)

## [1.2.3] - 2021-12-18

### Dependencies

- System.IO.Abstractions bump to 14.0.13

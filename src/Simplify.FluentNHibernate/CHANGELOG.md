# Changelog

## [3.2.0] - 2024-04-26

### Added

- PostgreSQL 8.3 dialect which uses timestamptz DateTime format

## [3.1.0] - 2024-03-24

### Added

- Microsoft.Data.SqlClient support (#489)

## [3.0.0] - 2024-01-14

### Added

- InitializeFromConfig* methods selectable dialect (#487)
- CompositeInterceptor (#483)
- Possibility to switch SqlStatementInterceptor from Trace.WriteLine to Console.WriteLine (#486)

### Changed

- ShowSql output by default redirected to Console
- Default PostgreSQL83 dialect set for PostgreSQL connections
- Default MsSql2012 dialect set for MS SQL connections
- InitializeFromConfigMsSql null password exception type change
- SessionFactoryBuilderBase dispose with GC.SuppressFinalize

### Dependencies

- FluentNHibernate bump to 3.3

## [2.6.0] - 2023-08-07

### Removed

- .NET 4.6.2 support

### Added

- .NET Standard 2.1 support
- .NET 4.8 support
- ConfigurationAddons parameter in ExportSchema as in UpdateSchema (#480)
- SessionFactoryBuilderBase moved from Simplify.Repository.FluentNHibernate (№481)

### Dependencies

- FluentNHibernate bump to 3.2.1
- Microsoft.Extensions.Configuration.Abstractions to 7

## [2.5.3] - 2023-04-15

### Dependencies

- FluentNHibernate bump to 3.2

## [2.5.2] - 2022-02-27

### Added

- Explicit .NET 6 support

### Fixed

- FluentConfigurationExtension schema update/export no error (#475)

### Dependencies

- Microsoft.Extensions.Configuration.Abstractions bump to 6

## [2.5.1] - 2021-10-27

### Fixed

- MsSQL provider: NRE when UserPassword is null (#310)

### Dependencies

- Switch to Microsoft.Extensions.Configuration.Abstractions 5.0.0 from Microsoft.Extensions.Configuration

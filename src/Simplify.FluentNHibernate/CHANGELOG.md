# Changelog

## [3.0.0] - 2024-01-14

### Added

- InitializeFromConfig* methods selectable dialect (#487)
- CompositeInterceptor (#483)

### Changed

- Default PostgreSQL83 dialect set for PostgreSQL connections
- Default MsSql2012 dialect set for MS SQL connections
- InitializeFromConfigMsSql null password exception
- SessionFactoryBuilderBase dispose with GC.SuppressFinalize

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

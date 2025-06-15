# Changelog

## [2.3.0] - 2025-06-15

### Removed

- .NET 6 support

### Added

- .NET 9 support

### Dependencies

- System.IO.Abstractions bump to 22.0.14

## [2.2.1] - 2023-08-26

### Added

- .NET Standard 2.1 support

### Dependencies

- System.IO.Abstractions bump to 19.2.67

## [2.2.0] - 2023-08-01

### Dependencies

- System.IO.Abstractions bump to 19.2.51
- Microsoft.Extensions.Configuration.Abstractions bump to 7.0

## [2.1.0] - 2022-11-30

### Added

- .NET 6.0 explicit targeting (removed incorrect .NET 5 targeting)

### Dependencies

- System.IO.Abstractions bump to 18.0.1 (PR#467)
- Microsoft.Extensions.Configuration.Abstractions bump to 6.0

## [2.0.3] - 2022-05-02

### Dependencies

- System.IO.Abstractions bump to 17.0.3 (PR#380)

## [2.0.2] - 2022-01-03

### Dependencies

- System.IO.Abstractions bump to 16.0.1 (PR#331)

## [2.0.1] - 2021-12-18

### Dependencies

- System.IO.Abstractions bump to 14.0.13

## [2.0.0] - 2021-10-26

### Removed

- `ConfigurationManagerBasedLoggerSettings`
- `_currentLogFileName` calculation based on `HttpContext.Current.Request.PhysicalApplicationPath` and `System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath`
- Default singleton

### Dependencies

- Microsoft.Extensions.Configuration switch to Microsoft.Extensions.Configuration.Abstactions 5.0.0
- System.IO.Abstractions bump to 13.2.47
- System.Web dependency dropped
- System.ServiceModel dependency dropped
- System.Configuration dependency dropped

### Added

- .NET Standard 2.0 support
- .NET 5 explicit support

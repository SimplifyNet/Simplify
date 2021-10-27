# Changelog

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
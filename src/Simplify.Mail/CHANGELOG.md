# Changelog

## [2.0] - 2025-11-20

### Breaking change

- Switched from System.Net.Mail to MailKit

### Removed

- System.Configuration.ConfigurationManager configuration support

### Dependencies

- Microsoft.Extensions.Configuration bump to 9.0.11

## [1.6.0] - 2025-06-15

### Removed

- .NET 6 support

### Added

- .NET 9 support

### Dependencies

- Microsoft.Extensions.Configuration bump to 9.0.6
- System.Configuration.ConfigurationManager bump to 9.0.6

## [1.5.5] - 2022-09-08

### Added

- .NET 6 support
- .NET Standard 2.1 support

### Dependencies

- Microsoft.Extensions.Configuration bump to 3.1.32

## [1.5.4] - 2022-09-08

### Fixed

- Collection was modified; enumeration operation may not execute.

## [1.5.3] - 2022-07-21

### Fixed

- Anti-spam pool made static (#416)

## [1.5.2] - 2022-04-29

### Fixed

- SmtpClient Disposing, MailSender is disposable

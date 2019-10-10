# Simplify

![Simplify](https://raw.githubusercontent.com/SimplifyNet/Images/master/Logo128x128.png)

[![Issues board](https://dxssrr2j0sq4w.cloudfront.net/3.2.0/img/external/zenhub-badge.svg)](https://app.zenhub.com/workspaces/simplify-5d7dd300da4a88000107f7e5/board?repos=208544410,208543783,208544195,208544168,208544390,208544370,208543999)
[![AppVeyor](https://img.shields.io/appveyor/ci/i4004/simplify/master)](https://ci.appveyor.com/project/i4004/simplify)
[![AppVeyor tests](https://img.shields.io/appveyor/tests/i4004/simplify/master)](https://ci.appveyor.com/project/i4004/simplify)
[![CodeFactor Grade](https://img.shields.io/codefactor/grade/github/SimplifyNet/Simplify)](https://www.codefactor.io/repository/github/simplifynet/simplify)
[![Dependabot Status](https://api.dependabot.com/badges/status?host=github&repo=SimplifyNet/Simplify)](https://dependabot.com)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen)](http://makeapullrequest.com)

`Simplify` is a set of .NET libraries that provide infrastructure for your applications. DI and mocking friendly.

## Packages

### Dependency Injection

#### Simplify.DI

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.DI)](https://www.nuget.org/packages/Simplify.DI)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.DI)](https://www.nuget.org/packages/Simplify.DI)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%201.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.DI)](https://libraries.io/nuget/Simplify.DI)
[![Documentation](https://img.shields.io/badge/docs-green)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.DI)

A common interface for IOC containers. Decouples users and frameworks (that are based on Simplify.DI) from dependency on IOC containers. Disciplines and unifies dependencies registration, verification and objects creation.


#### Simplify.DI IOC Containers Providers

##### CastleWindsor

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.DI.Provider.CastleWindsor)](https://www.nuget.org/packages/Simplify.DI.Provider.CastleWindsor)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.DI.Provider.CastleWindsor)](https://www.nuget.org/packages/Simplify.DI.Provider.CastleWindsor)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%201.6%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.DI.Provider.CastleWindsor)](https://libraries.io/nuget/Simplify.DI.Provider.CastleWindsor)

##### Microsoft.Extensions.DependencyInjection

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.DI.Provider.Microsoft.Extensions.DependencyInjection)](https://www.nuget.org/packages/Simplify.DI.Provider.Microsoft.Extensions.DependencyInjection)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.DI.Provider.Microsoft.Extensions.DependencyInjection)](https://www.nuget.org/packages/Simplify.DI.Provider.Microsoft.Extensions.DependencyInjection)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0%20%7C%20.NET%204.6.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.DI.Provider.Microsoft.Extensions.DependencyInjection)](https://libraries.io/nuget/Simplify.DI.Provider.Microsoft.Extensions.DependencyInjection)

##### SimpleInjector

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.DI.Provider.SimpleInjector)](https://www.nuget.org/packages/Simplify.DI.Provider.SimpleInjector)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.DI.Provider.SimpleInjector)](https://www.nuget.org/packages/Simplify.DI.Provider.SimpleInjector)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%201.3%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.DI.Provider.SimpleInjector)](https://libraries.io/nuget/Simplify.DI.Provider.SimpleInjector)

#### Simplify.DI Integrations

Packages which provides ability to use Simplify.DI as IOC container in some existing technologies.

##### Microsoft.Extensions.DependencyInjection

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection)](https://www.nuget.org/packages/Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection)](https://www.nuget.org/packages/Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection)](https://libraries.io/nuget/Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection)

##### WCF

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.DI.Wcf)](https://www.nuget.org/packages/Simplify.DI.Wcf)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.DI.Wcf)](https://www.nuget.org/packages/Simplify.DI.Wcf)
![Platform](https://img.shields.io/badge/platform-.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.DI.Wcf)](https://libraries.io/nuget/Simplify.DI.Wcf)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.DI.Wcf)

### Repositories & Databases

#### Simplify.FluentNHibernate

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.FluentNHibernate)](https://www.nuget.org/packages/Simplify.FluentNHibernate)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.FluentNHibernate)](https://www.nuget.org/packages/Simplify.FluentNHibernate)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0%20%7C%20.NET%204.6.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.FluentNHibernate)](https://libraries.io/nuget/Simplify.FluentNHibernate)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.FluentNHibernate)

FluentNHibernate easy configuration, session extensions and more.

#### Simplify.Repository

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Repository)](https://www.nuget.org/packages/Simplify.Repository)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Repository)](https://www.nuget.org/packages/Simplify.Repository)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%201.2%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Repository)](https://libraries.io/nuget/Simplify.Repository)

Generic Repository, Unit of Work patterns interfaces. Domain objects base interfaces.

#### Simplify.Repository.FluentNHibernate

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Repository.FluentNHibernate)](https://www.nuget.org/packages/Simplify.Repository.FluentNHibernate)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Repository.FluentNHibernate)](https://www.nuget.org/packages/Simplify.Repository.FluentNHibernate)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0%20%7C%20.NET%204.6.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Repository.FluentNHibernate)](https://libraries.io/nuget/Simplify.Repository.FluentNHibernate)

Simplify.Repository implementation for FluentNHibernate.

### Schedulers

#### Simplify.Scheduler

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Scheduler)](https://www.nuget.org/packages/Simplify.Scheduler)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Scheduler)](https://www.nuget.org/packages/Simplify.Scheduler)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Scheduler)](https://libraries.io/nuget/Simplify.Scheduler)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.Scheduler)

A scheduler services framework with DI. Allows you to simply create applications which can work on schedule.

#### Simplify.WindowsServices

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.WindowsServices)](https://www.nuget.org/packages/Simplify.WindowsServices)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.WindowsServices)](https://www.nuget.org/packages/Simplify.WindowsServices)
![Platform](https://img.shields.io/badge/platform-.NET%204.6.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.WindowsServices)](https://libraries.io/nuget/Simplify.WindowsServices)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.WindowsServices)

A scheduler Windows Services framework with DI. Allows you to simply create Windows Service based applications which can work on schedule.

### Main

#### Simplify.Log

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Log)](https://www.nuget.org/packages/Simplify.Log)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Log)](https://www.nuget.org/packages/Simplify.Log)
![Platform](https://img.shields.io/badge/platform-.NET%204.6.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Log)](https://libraries.io/nuget/Simplify.Log)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.Log)

Simple file-based logger.

#### Simplify.Mail

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Mail)](https://www.nuget.org/packages/Simplify.Mail)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Mail)](https://www.nuget.org/packages/Simplify.Mail)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0%20%7C%20.NET%204.6.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Mail)](https://libraries.io/nuget/Simplify.Mail)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.Mail)

SMTP mail sender with additional options and configuration.

#### Simplify.Pipelines

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Pipelines)](https://www.nuget.org/packages/Simplify.Pipelines)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Pipelines)](https://www.nuget.org/packages/Simplify.Pipelines)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%201.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Pipelines)](https://libraries.io/nuget/Simplify.Pipelines)

Сonveyor objects processing patterns interfaces and base classes.

#### Simplify.Resources

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Resources)](https://www.nuget.org/packages/Simplify.Resources)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Resources)](https://www.nuget.org/packages/Simplify.Resources)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Resources)](https://libraries.io/nuget/Simplify.Resources)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.Resources)

Package for getting localizable strings from assembly resource files.

#### Simplify.System

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.System)](https://www.nuget.org/packages/Simplify.System)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.System)](https://www.nuget.org/packages/Simplify.System)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.System)](https://libraries.io/nuget/Simplify.System)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.System)

Classes to get assembly information and ambient context for wrapping `DateTime.Now`, `DateTime.UtcNow`, `DateTime.Today` properties and more.

#### Simplify.System.Sources

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.System.Sources)](https://www.nuget.org/packages/Simplify.System.Sources)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.System.Sources)](https://www.nuget.org/packages/Simplify.System.Sources)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.System.Sources)](https://libraries.io/nuget/Simplify.System.Sources)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.System)

`Simplify.System` source code package for embedding.

#### Simplify.Templates

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Templates)](https://www.nuget.org/packages/Simplify.Templates)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Templates)](https://www.nuget.org/packages/Simplify.Templates)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Templates)](https://libraries.io/nuget/Simplify.Templates)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.Templates)

Text templates engine with fluent-interfaces, localization and more.

### Utility

#### Simplify.Cryptography

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Cryptography)](https://www.nuget.org/packages/Simplify.Cryptography)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Cryptography)](https://www.nuget.org/packages/Simplify.Cryptography)
![Platform](https://img.shields.io/badge/platform-4.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Cryptography)](https://libraries.io/nuget/Simplify.Cryptography)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.Cryptography)

Cryptography functions.

#### Simplify.Extensions

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Extensions)](https://www.nuget.org/packages/Simplify.Extensions)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Extensions)](https://www.nuget.org/packages/Simplify.Extensions)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%201.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Extensions)](https://libraries.io/nuget/Simplify.Extensions)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.Extensions)

System classes extensions.

#### Simplify.Extensions.Sources

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Extensions.Sources)](https://www.nuget.org/packages/Simplify.Extensions.Sources)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Extensions.Sources)](https://www.nuget.org/packages/Simplify.Extensions.Sources)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%201.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Extensions.Sources)](https://libraries.io/nuget/Simplify.Extensions.Sources)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.Extensions)

`Simplify.Extensions` source code package for embedding.

#### Simplify.IO

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.IO)](https://www.nuget.org/packages/Simplify.IO)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.IO)](https://www.nuget.org/packages/Simplify.IO)
![Platform](https://img.shields.io/badge/platform-.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.IO)](https://libraries.io/nuget/Simplify.IO)

IO utility functions.

#### Simplify.String

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.String)](https://www.nuget.org/packages/Simplify.String)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.String)](https://www.nuget.org/packages/Simplify.String)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.String)](https://libraries.io/nuget/Simplify.String)

String utility functions.

#### Simplify.String.Sources

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.String.Sources)](https://www.nuget.org/packages/Simplify.String.Sources)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.String.Sources)](https://www.nuget.org/packages/Simplify.String.Sources)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.String.Sources)](https://libraries.io/nuget/Simplify.String.Sources)

`Simplify.String` source code package for embedding.

#### Simplify.Xml

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Xml)](https://www.nuget.org/packages/Simplify.Xml)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Xml)](https://www.nuget.org/packages/Simplify.Xml)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Xml)](https://libraries.io/nuget/Simplify.Xml)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.Xml)

XML extension functions and serializer.

#### Simplify.Xml.Sources

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Xml.Sources)](https://www.nuget.org/packages/Simplify.Xml.Sources)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Xml.Sources)](https://www.nuget.org/packages/Simplify.Xml.Sources)
![Platform](https://img.shields.io/badge/platform-.NET%20Standard%202.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Xml.Sources)](https://libraries.io/nuget/Simplify.Xml.Sources)
[![Documentation](https://img.shields.io/badge/docs-green.svg)](https://github.com/SimplifyNet/Simplify/wiki/Simplify.Xml)

`Simplify.Xml` source code package for embedding.

### Desktop

#### Simplify.Windows.Forms

[![Nuget Version](https://img.shields.io/nuget/v/Simplify.Windows.Forms)](https://www.nuget.org/packages/Simplify.Windows.Forms)
[![Nuget Download](https://img.shields.io/nuget/dt/Simplify.Windows.Forms)](https://www.nuget.org/packages/Simplify.Windows.Forms)
![Platform](https://img.shields.io/badge/platform-.NET%20Core%203.0%20%7C%20.NET%204.5.2-lightgrey)
[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Windows.Forms)](https://libraries.io/nuget/Simplify.Windows.Forms)

`Simplify.Windows.Forms` controls set.

## Contributing

There are many ways in which you can participate in the project. Like most open-source software projects, contributing code is just one of many outlets where you can help improve. Some of the things that you could help out with are:

- Documentation (both code and features)
- Bug reports
- Bug fixes
- Feature requests
- Feature implementations
- Test coverage
- Code quality
- Sample applications

## License

Licensed under the GNU LESSER GENERAL PUBLIC LICENSE

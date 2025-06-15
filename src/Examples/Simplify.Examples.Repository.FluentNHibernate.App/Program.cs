using System;
using Simplify.DI;
using Simplify.Examples.Repository.FluentNHibernate.App.Infrastructure;
using Simplify.Examples.Repository.FluentNHibernate.App.Setup;


DIContainer.Current
	.RegisterAll()
	.Verify();

// App launch

var scope = DIContainer.Current.BeginLifetimeScope();

scope.Resolver.Resolve<ArgsHandler>().ProcessArgs(args);

Console.ReadLine();

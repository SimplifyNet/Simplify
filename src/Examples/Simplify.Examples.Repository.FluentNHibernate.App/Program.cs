using System;
using Simplify.DI;
using Simplify.Examples.Repository.FluentNHibernate.App.Infrastructure;
using Simplify.Examples.Repository.FluentNHibernate.App.Setup;

namespace Simplify.Examples.Repository.FluentNHibernate.App;

internal class Program
{
	private static void Main(string[] args)
	{
		// IOC container setup
		DIContainer.Current.RegisterSimplifyFluentNHibernateExamplesApp()
			.Verify();

		// App launch

		var scope = DIContainer.Current.BeginLifetimeScope();
		scope.Resolver.Resolve<ArgsHandler>().ProcessArgs(args);

		Console.ReadLine();
	}
}
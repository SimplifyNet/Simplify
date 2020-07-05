using System;
using Simplify.DI;
using Simplify.FluentNHibernate.Examples.App.Infrastructure;
using Simplify.FluentNHibernate.Examples.App.Setup;

namespace Simplify.FluentNHibernate.Examples.App
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			// IOC container setup

			//DIContainer.Current = new SimpleInjectorDIProvider();

			IocRegistrations.Register().Verify();

			var scope = DIContainer.Current.BeginLifetimeScope();

			scope.Resolver.Resolve<ArgsHandler>().ProcessArgs(args);

			Console.ReadLine();
		}
	}
}
using Simplify.DI;
using Simplify.Examples.Repository.FluentNHibernate.App.Infrastructure;
using Simplify.Examples.Repository.FluentNHibernate.App.Infrastructure.ConsoleImpl;

namespace Simplify.Examples.Repository.FluentNHibernate.App.Setup;

public static class InfrastructureRegistrations
{
	public static IDIRegistrator RegisterInfrastructure(this IDIRegistrator registrator) =>
		registrator.Register<INotifier, ConsoleNotifier>(LifetimeType.Singleton)
			.Register<IUserDisplayer, ConsoleUserDisplayer>()
			.Register<ArgsVerifier>(LifetimeType.Singleton)
			.Register<ArgsHandler>();
}
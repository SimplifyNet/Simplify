using Simplify.DI;
using Simplify.FluentNHibernate.Examples.App.Infrastructure;
using Simplify.FluentNHibernate.Examples.App.Infrastructure.ConsoleImpl;

namespace Simplify.FluentNHibernate.Examples.App.Setup
{
	public static class InfrastructureRegistrations
	{
		public static IDIRegistrator RegisterInfrastructure(this IDIRegistrator registrator)
		{
			return registrator.Register<INotifier, ConsoleNotifier>(LifetimeType.Singleton)
				.Register<IUserDisplayer, ConsoleUserDisplayer>()
				.Register<ArgsVerifier>(LifetimeType.Singleton)
				.Register<ArgsHandler>();
		}
	}
}
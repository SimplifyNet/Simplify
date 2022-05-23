using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.Scheduler.SimpleApp.Setup;

public static class IocRegistrations
{
	public static IConfiguration Configuration { get; private set; }

	public static IDIContainerProvider Register()
	{
		DIContainer.Current.RegisterConfiguration()
			.Register<PeriodicalProcessor>();

		return DIContainer.Current;
	}

	private static IDIRegistrator RegisterConfiguration(this IDIRegistrator registrator)
	{
		Configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", false)
			.Build();

		registrator.Register(p => Configuration, LifetimeType.Singleton);

		return registrator;
	}
}
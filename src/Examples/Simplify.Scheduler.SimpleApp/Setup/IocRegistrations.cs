using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.Scheduler.SimpleApp.Setup;

public static class IocRegistrations
{
	public static IConfiguration Configuration { get; private set; }

	public static IDIContainerProvider RegisterAll(this IDIContainerProvider provider)
	{
		provider.RegisterConfiguration()
			.Register<PeriodicalProcessor>();

		return provider;
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
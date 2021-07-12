using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.Examples.Repository.EntityFramework.App.Setup
{
	public static class ConfigurationRegistrations
	{
		public static IDIRegistrator RegisterConfiguration(this IDIRegistrator registrator) =>
			registrator.Register<IConfiguration>(p => new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false)
				.Build(), LifetimeType.Singleton);
	}
}
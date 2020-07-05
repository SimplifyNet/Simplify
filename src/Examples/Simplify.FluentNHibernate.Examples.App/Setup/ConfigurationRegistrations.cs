using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.FluentNHibernate.Examples.App.Setup
{
	public static class ConfigurationRegistrations
	{
		public static IDIRegistrator RegisterConfiguration(this IDIRegistrator registrator)
		{
			return registrator.Register<IConfiguration>(p => new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false)
				.Build(), LifetimeType.Singleton);
		}
	}
}
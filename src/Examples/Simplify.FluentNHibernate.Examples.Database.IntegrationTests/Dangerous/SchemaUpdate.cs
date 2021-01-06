using Microsoft.Extensions.Configuration;
using NHibernate.Cfg;
using NUnit.Framework;

namespace Simplify.FluentNHibernate.Examples.Database.IntegrationTests.Dangerous
{
	[TestFixture]
	[Category("Integration")]
	public class SchemaUpdate
	{
		[Test]
		public void UpdateSchema()
		{
			var cfg = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false)
				.Build();

			var configuration = new ExampleSessionFactoryBuilder(cfg).CreateConfiguration();

			Configuration config = null;
			configuration.ExposeConfiguration(c => config = c);
			configuration.BuildSessionFactory();

			config.CreateIndexesForForeignKeys();

			new NHibernate.Tool.hbm2ddl.SchemaUpdate(config).Execute(true, true);
		}
	}
}
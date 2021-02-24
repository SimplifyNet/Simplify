using Microsoft.Extensions.Configuration;
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

			new ExampleSessionFactoryBuilder(cfg).CreateConfiguration().UpdateSchema();
		}
	}
}
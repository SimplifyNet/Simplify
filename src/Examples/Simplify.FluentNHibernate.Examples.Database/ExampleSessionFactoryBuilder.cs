using FluentNHibernate.Cfg;
using Microsoft.Extensions.Configuration;
using Simplify.Repository.FluentNHibernate;

namespace Simplify.FluentNHibernate.Examples.Database
{
	public class ExampleSessionFactoryBuilder : SessionFactoryBuilderBase
	{
		public ExampleSessionFactoryBuilder(IConfiguration configuration, string configSectionName = "ExampleDatabaseConnectionSettings")
			: base(configuration, configSectionName)
		{
		}

		public override FluentConfiguration CreateConfiguration()
		{
			FluentConfiguration.InitializeFromConfigMsSql(Configuration, ConfigSectionName);
			FluentConfiguration.AddMappingsFromAssemblyOf<ExampleSessionFactoryBuilder>();

			return FluentConfiguration;
		}
	}
}
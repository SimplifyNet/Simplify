using FluentNHibernate.Cfg;
using Microsoft.Extensions.Configuration;
using Simplify.FluentNHibernate.Examples.Database.Mappings;
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
			FluentConfiguration.InitializeFromConfigMsSql(ConfigSectionName);
			FluentConfiguration.AddMappingsFromAssemblyOf<UserMap>();

			return FluentConfiguration;
		}
	}
}
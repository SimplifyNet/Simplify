using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.Configuration;
using Simplify.FluentNHibernate.Conventions;
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
			FluentConfiguration.AddMappingsFromAssemblyOf<ExampleSessionFactoryBuilder>(
				PrimaryKey.Name.Is(x => "ID"),
				Table.Is(x => x.EntityType.Name + "s"),
				ForeignKey.EndsWith("ID"),
				ForeignKeyConstraintNameConvention.WithConstraintNameConvention(),
				DefaultCascade.None());

			return FluentConfiguration;
		}
	}
}
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.Configuration;
using Simplify.FluentNHibernate;
using Simplify.FluentNHibernate.Conventions;

namespace Simplify.Examples.Repository.FluentNHibernate;

public class ExampleSessionFactoryBuilder(IConfiguration configuration, string configSectionName = "ExampleDatabaseConnectionSettings") : SessionFactoryBuilderBase(configuration, configSectionName)
{
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
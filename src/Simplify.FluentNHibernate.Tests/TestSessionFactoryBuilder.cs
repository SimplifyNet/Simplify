using FluentNHibernate.Cfg;
using Microsoft.Extensions.Configuration;

namespace Simplify.FluentNHibernate.Tests;

public class TestSessionFactoryBuilder : SessionFactoryBuilderBase
{
	public TestSessionFactoryBuilder(IConfiguration configuration) : base(configuration, "DatabaseConnectionSettings")
	{
	}

	public override FluentConfiguration CreateConfiguration()
	{
		var config = FluentConfiguration.InitializeFromConfigMsSql(Configuration, ConfigSectionName);

		config.AddMappingsFromAssemblyOf<TestSessionFactoryBuilder>();

		return config;
	}
}
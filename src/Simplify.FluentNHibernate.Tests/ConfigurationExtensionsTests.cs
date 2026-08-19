using FluentNHibernate.Cfg;
using Microsoft.Extensions.Configuration;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NUnit.Framework;

namespace Simplify.FluentNHibernate.Tests;

[TestFixture]
public class ConfigurationExtensionsTests
{
	private IConfiguration _configuration;

	[SetUp]
	public void Initialize()
	{
		_configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", false)
			.Build();
	}

	[Test]
	public void InitializeFromConfigOracleClient_CorrectConfig_NoExceptions()
	{
		// Act
		Fluently.Configure().InitializeFromConfigOracleClient(_configuration);
	}

	[Test]
	public void InitializeFromConfigOracleClient_SectionNotFound_DatabaseConnectionConfigurationException()
	{
		// Act
		Assert.Throws<DatabaseConnectionConfigurationException>(() => Fluently.Configure().InitializeFromConfigOracleClient("foo"));
	}

	[Test]
	public void InitializeFromConfigOracleOdpNetNative_CorrectConfig_NoException()
	{
		// Act
		Fluently.Configure().InitializeFromConfigOracleOdpNetNative(_configuration);
	}

	[Test]
	public void InitializeFromConfigOracleOdpNet_CorrectConfig_NoExceptions()
	{
		// Act
		Fluently.Configure().InitializeFromConfigOracleOdpNet(_configuration);
	}

	[Test]
	public void InitializeFromConfigMySql_CorrectConfig_NoExceptions()
	{
		// Act
		Fluently.Configure().InitializeFromConfigMySql(_configuration);
	}

	[Test]
	public void InitializeFromConfigMsSql_CorrectConfig_NoExceptions()
	{
		// Act
		Fluently.Configure().InitializeFromConfigMsSql(_configuration);
	}

	[Test]
	public void InitializeFromConfigMsSql_PortConfigured_ConnectionStringContainsPort()
	{
		// Arrange & Act
		Configuration config = null;
		Fluently.Configure()
			.InitializeFromConfigMsSql(_configuration)
			.ExposeConfiguration(c => config = c)
			.BuildConfiguration();

		// Assert
		Assert.That(config.GetProperty(Environment.ConnectionString), Does.Contain("localhost,1231"));
	}

	[Test]
	public void InitializeFromConfigMsSqlMicrosoftDriver_PortConfigured_ConnectionStringContainsPort()
	{
		// Arrange & Act
		Configuration config = null;
		Fluently.Configure()
			.InitializeFromConfigMsSqlMicrosoftDriver(_configuration)
			.ExposeConfiguration(c => config = c)
			.BuildConfiguration();

		// Assert
		Assert.That(config.GetProperty(Environment.ConnectionString), Does.Contain("localhost,1231"));
	}

	[Test]
	public void InitializeFromConfigSqLiteInMemory_CorrectConfig_NoExceptions()
	{
		// Act

		Fluently.Configure().InitializeFromConfigSqLiteInMemory(true);
		Fluently.Configure().InitializeFromConfigSqLiteInMemory(true, additionalClientConfiguration: c => c.Dialect<SQLiteDialect>());
	}
}
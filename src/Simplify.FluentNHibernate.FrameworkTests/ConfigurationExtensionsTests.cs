using FluentNHibernate.Cfg;
using Microsoft.Extensions.Configuration;
using NHibernate.Dialect;
using NUnit.Framework;

namespace Simplify.FluentNHibernate.FrameworkTests
{
	[TestFixture]
	public class ConfigurationExtensionsTests
	{
		private const string ConfigSectionName = "DatabaseConnectionSettings";

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

			Fluently.Configure().InitializeFromConfigOracleClient();
			Fluently.Configure().InitializeFromConfigOracleClient(ConfigSectionName, c => c.Dialect<Oracle10gDialect>());

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

			Fluently.Configure().InitializeFromConfigOracleOdpNetNative();
			Fluently.Configure().InitializeFromConfigOracleOdpNetNative(ConfigSectionName, c => c.Dialect<Oracle10gDialect>());

			Fluently.Configure().InitializeFromConfigOracleOdpNetNative(_configuration);
		}

		[Test]
		public void InitializeFromConfigOracleOdpNet_CorrectConfig_NoExceptions()
		{
			// Act

			Fluently.Configure().InitializeFromConfigOracleOdpNet();
			Fluently.Configure().InitializeFromConfigOracleOdpNet(ConfigSectionName, c => c.Dialect<Oracle10gDialect>());

			Fluently.Configure().InitializeFromConfigOracleOdpNet(_configuration);
		}

		[Test]
		public void InitializeFromConfigMySql_CorrectConfig_NoExceptions()
		{
			// Act

			Fluently.Configure().InitializeFromConfigMySql();
			Fluently.Configure().InitializeFromConfigMySql(ConfigSectionName, c => c.Dialect<MySQL5Dialect>());

			Fluently.Configure().InitializeFromConfigMySql(_configuration);
		}

		[Test]
		public void InitializeFromConfigMsSql_CorrectConfig_NoExceptions()
		{
			// Act

			Fluently.Configure().InitializeFromConfigMsSql();
			Fluently.Configure().InitializeFromConfigMsSql(ConfigSectionName, c => c.Dialect<MsSql2012Dialect>());

			Fluently.Configure().InitializeFromConfigMsSql(_configuration);
		}

		[Test]
		public void InitializeFromConfigSqLiteInMemory_CorrectConfig_NoExceptions()
		{
			// Act

			Fluently.Configure().InitializeFromConfigSqLiteInMemory(true);
			Fluently.Configure().InitializeFromConfigSqLiteInMemory(true, c => c.Dialect<SQLiteDialect>());
		}
	}
}
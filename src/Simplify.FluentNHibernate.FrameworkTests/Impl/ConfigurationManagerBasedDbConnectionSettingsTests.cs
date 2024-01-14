using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Simplify.FluentNHibernate.Settings;
using Simplify.FluentNHibernate.Settings.Impl;

namespace Simplify.FluentNHibernate.Tests.Settings.Impl;

[TestFixture]
public class ConfigurationManagerBasedDbConnectionSettingsTests
{
	[Test]
	public void InitializeFromConfigOracleClient_CorrectConfig_NoExceptions()
	{
		// Arrange & Act
		var settings = new ConfigurationManagerBasedDbConnectionSettings();

		// Assert

		Assert.That(settings.ServerName, Is.EqualTo("localhost"));
		Assert.That(settings.DataBaseName, Is.EqualTo("foodatabase"));
		Assert.That(settings.UserName, Is.EqualTo("foouser"));
		Assert.That(settings.UserPassword, Is.EqualTo("foopassword"));
		Assert.That(settings.ShowSql, Is.EqualTo(true));
		Assert.That(settings.ShowSqlOutputType, Is.EqualTo(ShowSqlOutputType.Trace));
		Assert.That(settings.Port, Is.EqualTo(1231));
	}
}
using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.FluentNHibernate.Settings.Impl;

/// <summary>
/// Provides ConfigurationManager based connection settings
/// </summary>
public class ConfigurationManagerBasedDbConnectionSettings : DbConnectionSettings
{
	/// <summary>
	/// Loads the specified configuration section name containing data-base connection settings.
	/// </summary>
	/// <param name="configSectionName">Name of the configuration section.</param>
	/// <exception cref="DatabaseConnectionConfigurationException"></exception>
	public ConfigurationManagerBasedDbConnectionSettings(string configSectionName = "DatabaseConnectionSettings")
	{
		if (string.IsNullOrEmpty(configSectionName)) throw new ArgumentNullException(nameof(configSectionName));

		var settings = (NameValueCollection)ConfigurationManager.GetSection(configSectionName)
			?? throw new DatabaseConnectionConfigurationException(
				$"Database connection section '{configSectionName}' was not found");

		ServerName = settings[nameof(ServerName)];
		DataBaseName = settings[nameof(DataBaseName)];
		UserName = settings[nameof(UserName)];
		UserPassword = settings[nameof(UserPassword)];
		var showSqlText = settings[nameof(ShowSql)];
		var showSqlOutputTypeText = settings[nameof(ShowSqlOutputType)];
		var port = settings[nameof(Port)];

		if (string.IsNullOrEmpty(ServerName))
			throw new DatabaseConnectionConfigurationException(
				$"Database connection section '{configSectionName}' ServerName property was not specified");

		if (string.IsNullOrEmpty(DataBaseName))
			throw new DatabaseConnectionConfigurationException(
				$"Database connection section '{configSectionName}' DataBaseName property was not specified");

		if (string.IsNullOrEmpty(UserName))
			throw new DatabaseConnectionConfigurationException(
				$"Database connection section '{configSectionName}' UserName property was not specified");

		if (!string.IsNullOrEmpty(showSqlText))
			if (bool.TryParse(showSqlText, out var buffer))
				ShowSql = buffer;

		if (!string.IsNullOrEmpty(showSqlOutputTypeText))
			if (Enum.TryParse<ShowSqlOutputType>(showSqlOutputTypeText, out var buffer))
				ShowSqlOutputType = buffer;

		if (!string.IsNullOrEmpty(port))
			if (int.TryParse(port, out var buffer))
				Port = buffer;
	}
}
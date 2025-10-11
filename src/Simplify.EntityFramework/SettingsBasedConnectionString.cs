using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Simplify.EntityFramework.Settings;
using Simplify.EntityFramework.Settings.Impl;

namespace Simplify.EntityFramework
{
	/// <summary>
	/// Provides connection string builder based on Simplify database connection settings style
	/// </summary>
	public static class SettingsBasedConnectionString
	{
		/// <summary>
		/// Builds the connections string based on DB settings from configuration.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configSectionName">Name of the configuration section.</param>
		/// <returns></returns>
		public static string Build(IConfiguration configuration, string configSectionName = "DatabaseConnectionSettings") => Build(new ConfigurationBasedDbConnectionSettings(configuration, configSectionName));

		/// <summary>
		/// Builds the connections string based on DbConnectionSettings.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <returns></returns>
		public static string Build(DbConnectionSettings settings)
		{
			return new SqlConnectionStringBuilder
			{
				DataSource = settings.ServerName,
				InitialCatalog = settings.DataBaseName,
				UserID = settings.UserName,
				Password = settings.UserPassword
			}.ConnectionString;
		}
	}
}
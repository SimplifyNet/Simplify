﻿using System;
using Microsoft.Extensions.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using NHibernate.Driver;
using Simplify.FluentNHibernate.Drivers;
using Simplify.FluentNHibernate.Settings;
using Simplify.FluentNHibernate.Settings.Impl;

namespace Simplify.FluentNHibernate;

/// <summary>
/// FluentNHibernate.Cfg.FluentConfiguration extensions
/// </summary>
public static class ConfigurationExtensions
{
	#region Oracle Client

	/// <summary>
	/// Initialize Oracle connection using Oracle10 client configuration and using oracle client to connect to database
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="configSectionName">Configuration section name in App.config or Web.config file.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">fluentConfiguration</exception>
	public static FluentConfiguration InitializeFromConfigOracleClient(this FluentConfiguration fluentConfiguration,
		string configSectionName = "DatabaseConnectionSettings",
		Action<OracleDataClientConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));

		InitializeFromConfigOracleClient(fluentConfiguration,
			new ConfigurationManagerBasedDbConnectionSettings(configSectionName),
			additionalClientConfiguration);

		return fluentConfiguration;
	}

	/// <summary>
	/// Initialize Oracle connection using Oracle10 client configuration and using oracle client to connect to database
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="configuration">The configuration containing database config section.</param>
	/// <param name="configSectionName">Database configuration section name in configuration.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">fluentConfiguration
	/// or
	/// configuration</exception>
	public static FluentConfiguration InitializeFromConfigOracleClient(this FluentConfiguration fluentConfiguration,
		IConfiguration configuration,
		string configSectionName = "DatabaseConnectionSettings",
		Action<OracleDataClientConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));
		if (configuration == null) throw new ArgumentNullException(nameof(configuration));

		InitializeFromConfigOracleClient(fluentConfiguration,
			new ConfigurationBasedDbConnectionSettings(configuration, configSectionName),
			additionalClientConfiguration);

		return fluentConfiguration;
	}

	private static void InitializeFromConfigOracleClient(FluentConfiguration fluentConfiguration,
		DbConnectionSettings settings,
		Action<OracleDataClientConfiguration>? additionalClientConfiguration = null)
	{
		var clientConfiguration = OracleDataClientConfiguration.Oracle10.ConnectionString(c =>
			c.Server(settings.ServerName)
				.Port(settings.Port ?? 1521)
				.Instance(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword));

		additionalClientConfiguration?.Invoke(clientConfiguration);

		fluentConfiguration.Database(clientConfiguration);

		PerformCommonInitialization(fluentConfiguration, settings.ShowSql);
	}

	#endregion Oracle Client

	#region ODP.NET Native

	/// <summary>
	/// Initialize Oracle connection using Oracle10 client configuration and using Oracle.DataAccess.dll to connect to database
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="configSectionName">Configuration section name in App.config or Web.config file.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">fluentConfiguration</exception>
	public static FluentConfiguration InitializeFromConfigOracleOdpNetNative(this FluentConfiguration fluentConfiguration,
		string configSectionName = "DatabaseConnectionSettings",
		Action<OracleDataClientConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));

		InitializeFromConfigOracleOdpNetNative(fluentConfiguration,
			new ConfigurationManagerBasedDbConnectionSettings(configSectionName),
			additionalClientConfiguration);

		return fluentConfiguration;
	}

	/// <summary>
	/// Initialize Oracle connection using Oracle10 client configuration and using Oracle.DataAccess.dll to connect to database
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="configuration">The configuration containing database config section.</param>
	/// <param name="configSectionName">Database configuration section name in configuration.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">
	/// fluentConfiguration
	/// or
	/// configuration
	/// </exception>
	public static FluentConfiguration InitializeFromConfigOracleOdpNetNative(this FluentConfiguration fluentConfiguration,
		IConfiguration configuration,
		string configSectionName = "DatabaseConnectionSettings",
		Action<OracleDataClientConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));
		if (configuration == null) throw new ArgumentNullException(nameof(configuration));

		InitializeFromConfigOracleOdpNetNative(fluentConfiguration,
			new ConfigurationBasedDbConnectionSettings(configuration, configSectionName),
			additionalClientConfiguration);

		return fluentConfiguration;
	}

	private static void InitializeFromConfigOracleOdpNetNative(FluentConfiguration fluentConfiguration,
		DbConnectionSettings settings,
		Action<OracleDataClientConfiguration>? additionalClientConfiguration = null)
	{
		var clientConfiguration = OracleDataClientConfiguration.Oracle10.ConnectionString(c => c
				.Server(settings.ServerName)
				.Port(settings.Port ?? 1521)
				.Instance(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword))
			.Driver<OracleDataClientDriverFix>();

		additionalClientConfiguration?.Invoke(clientConfiguration);

		fluentConfiguration.Database(clientConfiguration);

		PerformCommonInitialization(fluentConfiguration, settings.ShowSql);
	}

	#endregion ODP.NET Native

	#region ODP.NET

	/// <summary>
	/// Initialize Oracle connection using Oracle10 client configuration and using Oracle.ManagedDataAccess.dll to connect to database
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="configSectionName">Configuration section name in App.config or Web.config file.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">fluentConfiguration</exception>
	public static FluentConfiguration InitializeFromConfigOracleOdpNet(this FluentConfiguration fluentConfiguration,
		string configSectionName = "DatabaseConnectionSettings",
		Action<OracleManagedDataClientConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));

		InitializeFromConfigOracleOdpNet(fluentConfiguration,
			new ConfigurationManagerBasedDbConnectionSettings(configSectionName),
			additionalClientConfiguration);

		return fluentConfiguration;
	}

	/// <summary>
	/// Initialize Oracle connection using Oracle10 client configuration and using Oracle.ManagedDataAccess.dll to connect to database
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="configuration">The configuration containing database config section.</param>
	/// <param name="configSectionName">Database configuration section name in configuration.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">
	/// fluentConfiguration
	/// or
	/// configuration
	/// </exception>
	public static FluentConfiguration InitializeFromConfigOracleOdpNet(this FluentConfiguration fluentConfiguration,
		IConfiguration configuration,
		string configSectionName = "DatabaseConnectionSettings",
		Action<OracleManagedDataClientConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));
		if (configuration == null) throw new ArgumentNullException(nameof(configuration));

		InitializeFromConfigOracleOdpNet(fluentConfiguration,
			new ConfigurationBasedDbConnectionSettings(configuration, configSectionName),
			additionalClientConfiguration);

		return fluentConfiguration;
	}

	private static void InitializeFromConfigOracleOdpNet(FluentConfiguration fluentConfiguration,
		DbConnectionSettings settings,
		Action<OracleManagedDataClientConfiguration>? additionalClientConfiguration = null)
	{
		var clientConfiguration = OracleManagedDataClientConfiguration.Oracle10.ConnectionString(c => c
				.Server(settings.ServerName)
				.Port(settings.Port ?? 1521)
				.Instance(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword))
			.Driver<OracleManagedDataClientDriver>();

		additionalClientConfiguration?.Invoke(clientConfiguration);

		fluentConfiguration.Database(clientConfiguration);

		PerformCommonInitialization(fluentConfiguration, settings.ShowSql);
	}

	#endregion ODP.NET

	#region MySQL

	/// <summary>
	/// Initialize MySQL connection using Standard client configuration
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="configSectionName">Configuration section name in App.config or Web.config file.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">fluentConfiguration</exception>
	public static FluentConfiguration InitializeFromConfigMySql(this FluentConfiguration fluentConfiguration,
		string configSectionName = "DatabaseConnectionSettings",
		Action<MySQLConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));

		InitializeFromConfigMySql(fluentConfiguration,
			new ConfigurationManagerBasedDbConnectionSettings(configSectionName),
			additionalClientConfiguration);

		return fluentConfiguration;
	}

	/// <summary>
	/// Initialize MySQL connection using Standard client configuration
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="configuration">The configuration containing database config section.</param>
	/// <param name="configSectionName">Database configuration section name in configuration.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">
	/// fluentConfiguration
	/// or
	/// configuration
	/// </exception>
	public static FluentConfiguration InitializeFromConfigMySql(this FluentConfiguration fluentConfiguration,
		IConfiguration configuration,
		string configSectionName = "DatabaseConnectionSettings",
		Action<MySQLConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));
		if (configuration == null) throw new ArgumentNullException(nameof(configuration));

		InitializeFromConfigMySql(fluentConfiguration,
			new ConfigurationBasedDbConnectionSettings(configuration, configSectionName),
			additionalClientConfiguration);

		return fluentConfiguration;
	}

	private static void InitializeFromConfigMySql(FluentConfiguration fluentConfiguration,
		DbConnectionSettings settings,
		Action<MySQLConfiguration>? additionalClientConfiguration = null)
	{
		var clientConfiguration = MySQLConfiguration.Standard.ConnectionString(c => c
			.Server(settings.ServerName)
			.Database(settings.DataBaseName)
			.Username(settings.UserName)
			.Password(settings.UserPassword));

		additionalClientConfiguration?.Invoke(clientConfiguration);

		fluentConfiguration.Database(clientConfiguration);

		PerformCommonInitialization(fluentConfiguration, settings.ShowSql);
	}

	#endregion MySQL

	#region MS SQL

	/// <summary>
	/// Initialize MsSQL connection using MsSql2008 client configuration
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="configSectionName">Configuration section name in App.config or Web.config file.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">fluentConfiguration</exception>
	public static FluentConfiguration InitializeFromConfigMsSql(this FluentConfiguration fluentConfiguration,
		string configSectionName = "DatabaseConnectionSettings",
		Action<MsSqlConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));

		InitializeFromConfigMsSql(fluentConfiguration,
			new ConfigurationManagerBasedDbConnectionSettings(configSectionName),
			additionalClientConfiguration);

		return fluentConfiguration;
	}

	/// <summary>
	/// Initialize MsSQL connection using MsSql2008 client configuration
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="configuration">The configuration containing database config section.</param>
	/// <param name="configSectionName">Database configuration section name in configuration.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">
	/// fluentConfiguration
	/// or
	/// configuration
	/// </exception>
	public static FluentConfiguration InitializeFromConfigMsSql(this FluentConfiguration fluentConfiguration,
		IConfiguration configuration,
		string configSectionName = "DatabaseConnectionSettings",
		Action<MsSqlConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));
		if (configuration == null) throw new ArgumentNullException(nameof(configuration));

		InitializeFromConfigMsSql(fluentConfiguration,
			new ConfigurationBasedDbConnectionSettings(configuration, configSectionName),
			additionalClientConfiguration);

		return fluentConfiguration;
	}

	private static void InitializeFromConfigMsSql(FluentConfiguration fluentConfiguration,
		DbConnectionSettings settings,
		Action<MsSqlConfiguration>? additionalClientConfiguration = null)
	{
		var clientConfiguration = MsSqlConfiguration.MsSql2008.ConnectionString(c => c
			.Server(settings.ServerName)
			.Database(settings.DataBaseName)
			.Username(settings.UserName)
			.Password(settings.UserPassword ?? throw new ArgumentNullException(nameof(settings.UserPassword))));

		additionalClientConfiguration?.Invoke(clientConfiguration);

		fluentConfiguration.Database(clientConfiguration);

		PerformCommonInitialization(fluentConfiguration, settings.ShowSql);
	}

	#endregion MS SQL

	#region PostgreSQL

	/// <summary>
	/// Initialize PostgreSQL connection using PostgreSQL82 client configuration
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="configSectionName">Configuration section name in App.config or Web.config file.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">fluentConfiguration</exception>
	public static FluentConfiguration InitializeFromConfigPostgreSql(this FluentConfiguration fluentConfiguration,
		string configSectionName = "DatabaseConnectionSettings",
		Action<PostgreSQLConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));

		InitializeFromConfigPostgreSql(fluentConfiguration,
			new ConfigurationManagerBasedDbConnectionSettings(configSectionName),
			additionalClientConfiguration);

		return fluentConfiguration;
	}

	/// <summary>
	/// Initialize PostgreSQL connection using PostgreSQL82 client configuration
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="configuration">The configuration containing database config section.</param>
	/// <param name="configSectionName">Database configuration section name in configuration.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">
	/// fluentConfiguration
	/// or
	/// configuration
	/// </exception>
	public static FluentConfiguration InitializeFromConfigPostgreSql(this FluentConfiguration fluentConfiguration,
		IConfiguration configuration,
		string configSectionName = "DatabaseConnectionSettings",
		Action<PostgreSQLConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));
		if (configuration == null) throw new ArgumentNullException(nameof(configuration));

		InitializeFromConfigPostgreSql(fluentConfiguration,
			new ConfigurationBasedDbConnectionSettings(configuration, configSectionName),
			additionalClientConfiguration);

		return fluentConfiguration;
	}

	private static void InitializeFromConfigPostgreSql(FluentConfiguration fluentConfiguration,
		DbConnectionSettings settings,
		Action<PostgreSQLConfiguration>? additionalClientConfiguration = null)
	{
		var clientConfiguration = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(c => c
			.Host(settings.ServerName)
			.Port(settings.Port ?? 5432)
			.Database(settings.DataBaseName)
			.Username(settings.UserName)
			.Password(settings.UserPassword));

		additionalClientConfiguration?.Invoke(clientConfiguration);

		fluentConfiguration.Database(clientConfiguration);

		PerformCommonInitialization(fluentConfiguration, settings.ShowSql);
	}

	#endregion PostgreSQL

	#region SQLite

	/// <summary>
	/// Initialize SqLite connection using Standard client configuration
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="fileName">Name of the SqLite database file.</param>
	/// <param name="showSql">if set to <c>true</c> then all executed SQL queries will be shown in trace window.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">
	/// fluentConfiguration
	/// or
	/// fileName
	/// </exception>
	public static FluentConfiguration InitializeFromConfigSqLite(this FluentConfiguration fluentConfiguration,
		string fileName,
		bool showSql = false,
		Action<SQLiteConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));
		if (fileName == null) throw new ArgumentNullException(nameof(fileName));

		var clientConfiguration = SQLiteConfiguration.Standard.UsingFile(fileName);

		additionalClientConfiguration?.Invoke(clientConfiguration);

		fluentConfiguration.Database(clientConfiguration);

		PerformCommonInitialization(fluentConfiguration, showSql);

		return fluentConfiguration;
	}

	#endregion SQLite

	#region SQLite In-Memory

	/// <summary>
	/// Initialize SqLite connection using in memory database
	/// </summary>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="showSql">if set to <c>true</c> then all executed SQL queries will be shown in trace window.</param>
	/// <param name="additionalClientConfiguration">The additional client configuration.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">fluentConfiguration</exception>
	public static FluentConfiguration InitializeFromConfigSqLiteInMemory(this FluentConfiguration fluentConfiguration,
		bool showSql = false,
		Action<SQLiteConfiguration>? additionalClientConfiguration = null)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));

		var clientConfiguration = SQLiteConfiguration.Standard.InMemory();

		additionalClientConfiguration?.Invoke(clientConfiguration);

		fluentConfiguration.Database(clientConfiguration);

		PerformCommonInitialization(fluentConfiguration, showSql);

		return fluentConfiguration;
	}

	#endregion SQLite In-Memory

	/// <summary>
	/// Adds the mappings from assembly of specified type.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
	/// <param name="conventions">The conventions.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">fluentConfiguration</exception>
	public static FluentConfiguration AddMappingsFromAssemblyOf<T>(this FluentConfiguration fluentConfiguration, params IConvention[] conventions)
	{
		if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));

		fluentConfiguration.Mappings(m => m.FluentMappings
			.AddFromAssemblyOf<T>()
			.Conventions.Add(conventions));

		return fluentConfiguration;
	}

	private static void PerformCommonInitialization(FluentConfiguration fluentConfiguration, bool showSql)
	{
		fluentConfiguration.ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"));

		if (showSql)
			fluentConfiguration.ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()));
	}
}
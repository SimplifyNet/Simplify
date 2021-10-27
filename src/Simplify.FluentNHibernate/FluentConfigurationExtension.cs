using System;
using FluentNHibernate.Cfg;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Simplify.FluentNHibernate
{
	/// <summary>
	/// FluentConfiguration extensions
	/// </summary>
	public static class FluentConfigurationExtension
	{
		/// <summary>
		/// Creates database structure from code (destructive)
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public static void ExportSchema(this FluentConfiguration configuration)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			Configuration? config = null;
			configuration.ExposeConfiguration(c => config = c);
			var factory = configuration.BuildSessionFactory();

			var export = new SchemaExport(config);
			export.Execute(false, true, false, factory.OpenSession().Connection, null);
		}

		/// <summary>
		/// Updates database structure from code (creates missing tables, columns, foreign keys) (non-destructive).
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configurationAddons">The configuration addition setup.</param>
		/// <exception cref="ArgumentNullException">configuration</exception>
		public static void UpdateSchema(this FluentConfiguration configuration, Action<Configuration>? configurationAddons = null)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			Configuration? config = null;
			configuration.ExposeConfiguration(c => config = c);
			configuration.BuildSessionFactory();

			configurationAddons?.Invoke(config!);

			var updater = new SchemaUpdate(config);
			updater.Execute(true, true);
		}
	}
}
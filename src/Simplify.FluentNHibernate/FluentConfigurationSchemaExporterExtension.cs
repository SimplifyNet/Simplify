using System;

using FluentNHibernate.Cfg;

using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Simplify.FluentNHibernate
{
	/// <summary>
	/// Entities to database exporter extensions
	/// </summary>
	public static class FluentConfigurationSchemaExporterExtension
	{
		/// <summary>
		/// Create database structure from code
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public static void ExportSchema(this FluentConfiguration configuration)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			Configuration config = null;
			configuration.ExposeConfiguration(c => config = c);
			var factory = configuration.BuildSessionFactory();

			var export = new SchemaExport(config);
			export.Execute(false, true, false, factory.OpenSession().Connection, null);
		}
	}
}
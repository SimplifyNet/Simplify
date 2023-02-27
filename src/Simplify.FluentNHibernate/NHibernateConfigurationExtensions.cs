using System.Collections.Generic;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Mapping;

namespace Simplify.FluentNHibernate;

/// <summary>
/// Provides NHibernate configuration extensions
/// </summary>
public static class NHibernateConfigurationExtensions
{
	private static readonly PropertyInfo TableMappingsProperty =
		typeof(Configuration).GetProperty("TableMappings", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!;

	/// <summary>
	/// Creates the indexes for foreign keys.
	/// </summary>
	/// <param name="configuration">The configuration.</param>
	public static void CreateIndexesForForeignKeys(this Configuration configuration)
	{
		configuration.BuildMappings();

		var tables = (ICollection<Table>?)TableMappingsProperty.GetValue(configuration, null) ?? throw new System.InvalidOperationException("TableMappings value is null");

		foreach (var table in tables)
		{
			foreach (var foreignKey in table.ForeignKeyIterator)
			{
				var idx = new Index();

				idx.AddColumns(foreignKey.ColumnIterator);
				idx.Name = "IDX_" + foreignKey.Name;
				idx.Table = table;

				table.AddIndex(idx);
			}
		}
	}
}
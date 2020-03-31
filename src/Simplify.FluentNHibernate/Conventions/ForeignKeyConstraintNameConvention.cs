﻿using System.Linq;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Simplify.FluentNHibernate.Conventions
{
	/// <summary>
	/// Provides foreign key constraint naming for ManyToMany and ManyToOne relations
	/// </summary>
	public class ForeignKeyConstraintNameConvention : IHasManyToManyConvention, IReferenceConvention
	{
		/// <summary>
		/// Adds the foreign key constraint name convention.
		///
		/// Note: 'HasMany' with 'Element' foreign key constraint name is not supported, it should be set manually by 'ForeignKeyConstraintName'
		/// </summary>
		/// <returns></returns>
		public static ForeignKeyConstraintNameConvention WithConstraintNameConvention()
		{
			return new ForeignKeyConstraintNameConvention();
		}

		/// <summary>
		/// Applies the IManyToOneInstance instance.
		/// </summary>
		/// <param name="instance">The instance.</param>
		public void Apply(IManyToOneInstance instance)
		{
			instance.ForeignKey(
				GetForeignConstraintName(
					instance.EntityType.Name,
					instance.Columns.First().Name));
		}

		/// <summary>
		/// Applies the IManyToManyCollectionInstance instance.
		/// </summary>
		/// <param name="instance">The instance.</param>
		public void Apply(IManyToManyCollectionInstance instance)
		{
			instance.Relationship?.ForeignKey(
				GetForeignConstraintName(
					instance.TableName,
					instance.Relationship.Columns.First().Name));
		}

		private static string GetForeignConstraintName(string targetTableName, string sourceTableColumnName)
		{
			return $"FK_{targetTableName}_{sourceTableColumnName}";
		}
	}
}
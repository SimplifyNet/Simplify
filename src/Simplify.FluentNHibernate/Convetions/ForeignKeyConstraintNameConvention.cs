using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System.Linq;

namespace Simplify.FluentNHibernate.Convetions
{
	/// <summary>
	/// Provides foreign key constraint naming for ManyToMany and ManyToOne relations
	/// </summary>
	public class ForeignKeyConstraintNameConvention : IHasManyToManyConvention, IReferenceConvention
	{
		/// <summary>
		/// Withes the foreign key constraint name convention.
		///
		/// Note: For 'HasMany' with 'Element' foreign key constraint name should be set by 'ForeignKeyConstraintName'
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
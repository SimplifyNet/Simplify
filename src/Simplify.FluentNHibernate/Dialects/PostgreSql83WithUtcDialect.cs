using System.Data;
using NHibernate.Dialect;

namespace Simplify.FluentNHibernate.Dialects;

/// <summary>
/// Provides PostgreSQL dialect which uses timestamptz DateTime format
/// </summary>
public class PostgreSql83ZDialect : PostgreSQL83Dialect
{
	/// <summary>
	/// Initialize PostgreSql83WithUtcDialect
	/// </summary>
	public PostgreSql83ZDialect() => RegisterColumnType(DbType.DateTime, "timestamptz");
}
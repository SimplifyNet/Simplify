namespace Simplify.FluentNHibernate.Dialects;

/// <summary>
/// Provides PostgreSQL dialect
/// </summary>
public enum PostgreSqlDialect
{
	/// <summary>
	/// The standard dialect
	/// </summary>
	Standard,
	/// <summary>
	/// The PostgreSQL 8.1 dialect
	/// </summary>
	PostgreSQL81,
	/// <summary>
	/// The PostgreSQL 8.2 dialect
	/// </summary>
	PostgreSQL82,
	/// <summary>
	/// The PostgreSQL 8.3 dialect
	/// </summary>
	PostgreSQL83
}
namespace Simplify.FluentNHibernate.Dialects;

/// <summary>
/// Provides PostgreSQL DBMS dialect
/// </summary>
public enum PostgreSqlDialect
{
	/// <summary>
	/// The Standard dialect
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
	PostgreSQL83,

	/// <summary>
	/// The PostgreSQL 8.3 dialect using timestamptz DateTime format
	/// </summary>
	PostgreSQL83Z
}
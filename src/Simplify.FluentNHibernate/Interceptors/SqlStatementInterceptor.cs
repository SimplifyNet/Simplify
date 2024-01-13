using System.Diagnostics;
using NHibernate;
using NHibernate.SqlCommand;

namespace Simplify.FluentNHibernate.Interceptors;

/// <summary>
/// Executed SQL code tracer
/// </summary>
public class SqlStatementInterceptor : EmptyInterceptor
{
	/// <summary>
	/// Called on sql statement prepare.
	/// </summary>
	/// <param name="sql">The SQL.</param>
	/// <returns></returns>
	public override SqlString OnPrepareStatement(SqlString sql)
	{
		Trace.WriteLine($"SQL executed: '{sql}'");

		return sql;
	}
}
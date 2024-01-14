using System;
using System.Diagnostics;
using NHibernate;
using NHibernate.SqlCommand;
using Simplify.FluentNHibernate.Settings;

namespace Simplify.FluentNHibernate.Interceptors;

/// <summary>
/// Executed SQL code tracer
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SqlStatementInterceptor"/> class.
/// </remarks>
/// <param name="showSqlOutputType">Specifies the SQL commands output type</param>
public class SqlStatementInterceptor(ShowSqlOutputType showSqlOutputType) : EmptyInterceptor
{
	private readonly ShowSqlOutputType _sqlSource = showSqlOutputType;

	/// <summary>
	/// Called on sql statement prepare.
	/// </summary>
	/// <param name="sql">The SQL.</param>
	/// <returns></returns>
	public override SqlString OnPrepareStatement(SqlString sql)
	{
		var message = $"SQL executed: '{sql}'";

		switch (_sqlSource)
		{
			case ShowSqlOutputType.Console:
				Console.WriteLine(message);
				break;

			case ShowSqlOutputType.Trace:
				Trace.WriteLine(message);
				break;
		}

		return sql;
	}
}
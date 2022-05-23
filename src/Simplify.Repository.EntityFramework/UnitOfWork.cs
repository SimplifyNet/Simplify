using System;
using Microsoft.EntityFrameworkCore;

namespace Simplify.Repository.EntityFramework;

/// <summary>
/// Provides unit of work
/// </summary>
public class UnitOfWork<T> : IUnitOfWork
	where T : DbContext
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UnitOfWork{T}" /> class.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <exception cref="System.InvalidOperationException">Error opening session, session is null</exception>
	public UnitOfWork(DbContext context) => Context = context ?? throw new InvalidOperationException("Error opening session, session is null");

	/// <summary>
	/// Gets the session.
	/// </summary>
	/// <value>
	/// The session.
	/// </value>
	public DbContext Context { get; }

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Releases unmanaged and - optionally - managed resources.
	/// </summary>
	/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!disposing)
			return;

		Context.Dispose();
	}
}
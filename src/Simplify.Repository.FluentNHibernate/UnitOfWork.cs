using System;
using NHibernate;

namespace Simplify.Repository.FluentNHibernate;

/// <summary>
/// Provides unit of work
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UnitOfWork"/> class.
/// </remarks>
/// <param name="sessionFactory">The session factory.</param>
public class UnitOfWork(ISessionFactory sessionFactory) : IUnitOfWork
{

	/// <summary>
	/// Gets the session.
	/// </summary>
	/// <value>
	/// The session.
	/// </value>
	public ISession Session { get; protected set; } = sessionFactory.OpenSession() ?? throw new InvalidOperationException("Error opening session, session is null");

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

		Session.Dispose();
	}
}
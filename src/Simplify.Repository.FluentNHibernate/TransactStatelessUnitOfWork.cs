using System.Data;
using System.Threading.Tasks;
using NHibernate;

namespace Simplify.Repository.FluentNHibernate;

/// <summary>
///  Provides unit of work with manual stateless session open transaction
/// </summary>
/// <seealso cref="IUnitOfWork" />
/// <remarks>
/// Initializes a new instance of the <see cref="StatelessUnitOfWork"/> class.
/// </remarks>
/// <param name="sessionFactory">The session factory.</param>
public class TransactStatelessUnitOfWork(ISessionFactory sessionFactory) : StatelessUnitOfWork(sessionFactory), ITransactUnitOfWork
{
	private readonly UnitOfWorkTransaction _transaction = new();

	/// <inheritdoc />
	public bool IsTransactionActive => _transaction.IsActive;

	/// <inheritdoc />
	public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) =>
		_transaction.Begin(Session.BeginTransaction(isolationLevel));

	/// <inheritdoc />
	public virtual void Commit() => _transaction.Commit();

	/// <inheritdoc />
	public Task CommitAsync() => _transaction.CommitAsync();

	/// <inheritdoc />
	public virtual void Rollback() => _transaction.Rollback();

	/// <inheritdoc />
	public Task RollbackAsync() => _transaction.RollbackAsync();

	/// <summary>
	/// Releases unmanaged and - optionally - managed resources.
	/// </summary>
	/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
	protected override void Dispose(bool disposing)
	{
		if (!disposing)
			return;

		_transaction.Dispose();

		base.Dispose(disposing);
	}
}

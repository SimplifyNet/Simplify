using System;
using System.Threading.Tasks;
using NHibernate;

namespace Simplify.Repository.FluentNHibernate;

/// <summary>
/// Encapsulates the manual NHibernate transaction lifecycle.
/// </summary>
internal sealed class UnitOfWorkTransaction : IDisposable
{
	private ITransaction? _transaction;

	/// <summary>
	/// Gets a value indicating whether a transaction is active.
	/// </summary>
	public bool IsActive => _transaction != null;

	/// <summary>
	/// Begins the transaction.
	/// </summary>
	/// <param name="transaction">The transaction to track.</param>
	/// <exception cref="InvalidOperationException">Oops! We already have an active transaction</exception>
	public void Begin(ITransaction transaction)
	{
		if (_transaction is { IsActive: true })
			throw new InvalidOperationException("Oops! We already have an active transaction");

		_transaction = transaction;
	}

	/// <summary>
	/// Commits transaction.
	/// </summary>
	/// <exception cref="InvalidOperationException">Oops! We don't have an active transaction</exception>
	public void Commit()
	{
		EnsureActive();

		_transaction!.Commit();
		Clear();
	}

	/// <summary>
	/// Commits transaction asynchronously.
	/// </summary>
	/// <exception cref="InvalidOperationException">Oops! We don't have an active transaction</exception>
	public async Task CommitAsync()
	{
		EnsureActive();

		await _transaction!.CommitAsync();
		Clear();
	}

	/// <summary>
	/// Rollbacks transaction.
	/// </summary>
	/// <exception cref="InvalidOperationException">Oops! We don't have an active transaction</exception>
	public void Rollback()
	{
		EnsureActive();

		_transaction!.Rollback();
		Clear();
	}

	/// <summary>
	/// Rollbacks transaction asynchronously.
	/// </summary>
	/// <exception cref="InvalidOperationException">Oops! We don't have an active transaction</exception>
	public async Task RollbackAsync()
	{
		EnsureActive();

		await _transaction!.RollbackAsync();
		Clear();
	}

	/// <summary>
	/// Disposes the underlying transaction, if any.
	/// </summary>
	public void Dispose() => _transaction?.Dispose();

	private void EnsureActive()
	{
		if (_transaction == null || !_transaction.IsActive)
			throw new InvalidOperationException("Oops! We don't have an active transaction");
	}

	private void Clear()
	{
		_transaction!.Dispose();
		_transaction = null;
	}
}

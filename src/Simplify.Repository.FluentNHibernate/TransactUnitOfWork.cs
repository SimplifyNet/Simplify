using System;
using System.Data;
using System.Threading.Tasks;
using NHibernate;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides unit of work with manual open transaction
	/// </summary>
	public class TransactUnitOfWork : UnitOfWork, ITransactUnitOfWork
	{
		private ITransaction? _transaction;

		/// <summary>
		/// Initializes a new instance of the <see cref="TransactUnitOfWork"/> class.
		/// </summary>
		/// <param name="sessionFactory">The session factory.</param>
		public TransactUnitOfWork(ISessionFactory sessionFactory) : base(sessionFactory)
		{
		}

		/// <summary>
		/// Gets a value indicating whether UoW's transaction is active.
		/// </summary>
		/// <value>
		///  <c>true</c> if UoW's transaction active; otherwise, <c>false</c>.
		/// </value>
		public bool IsTransactionActive => _transaction != null;

		/// <summary>
		/// Begins the transaction.
		/// </summary>
		/// <param name="isolationLevel">The isolation level.</param>
		public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) => _transaction = Session.BeginTransaction(isolationLevel);

		/// <summary>
		/// Commits transaction.
		/// </summary>
		/// <exception cref="InvalidOperationException">Oops! We don't have an active transaction</exception>
		public virtual void Commit()
		{
			if (_transaction == null || !_transaction.IsActive)
				throw new InvalidOperationException("Oops! We don't have an active transaction");

			_transaction.Commit();
			_transaction.Dispose();
			_transaction = null;
		}

		/// <summary>
		/// Commits transaction asynchronously.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException">Oops! We don't have an active transaction</exception>
		public async Task CommitAsync()
		{
			if (_transaction == null || !_transaction.IsActive)
				throw new InvalidOperationException("Oops! We don't have an active transaction");

			await _transaction.CommitAsync();
			_transaction.Dispose();
			_transaction = null;
		}

		/// <summary>
		/// Rollbacks transaction.
		/// </summary>
		public virtual void Rollback()
		{
			if (_transaction == null || !_transaction.IsActive)
				throw new InvalidOperationException("Oops! We don't have an active transaction");

			_transaction.Rollback();
			_transaction.Dispose();
			_transaction = null;
		}

		/// <summary>
		/// Rollbacks transaction asynchronously.
		/// </summary>
		/// <returns></returns>
		public async Task RollbackAsync()
		{
			if (_transaction == null || !_transaction.IsActive)
				throw new InvalidOperationException("Oops! We don't have an active transaction");

			await _transaction.RollbackAsync();
			_transaction.Dispose();
			_transaction = null;
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (!disposing)
				return;

			_transaction?.Dispose();

			base.Dispose(disposing);
		}
	}
}
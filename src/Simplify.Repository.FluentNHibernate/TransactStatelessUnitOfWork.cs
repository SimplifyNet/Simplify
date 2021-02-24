using System;
using System.Data;
using System.Threading.Tasks;
using NHibernate;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	///  Provides unit of work with manual stateless session open transaction
	/// </summary>
	/// <seealso cref="IUnitOfWork" />
	public class TransactStatelessUnitOfWork : StatelessUnitOfWork, ITransactUnitOfWork
	{
		private ITransaction? _transaction;

		/// <summary>
		/// Initializes a new instance of the <see cref="StatelessUnitOfWork"/> class.
		/// </summary>
		/// <param name="sessionFactory">The session factory.</param>
		public TransactStatelessUnitOfWork(ISessionFactory sessionFactory) : base(sessionFactory)
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
		public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) => _transaction = Session.BeginTransaction(isolationLevel);

		/// <summary>
		/// Commits transaction.
		/// </summary>
		/// <exception cref="InvalidOperationException">Oops! We don't have an active transaction</exception>
		public void Commit()
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
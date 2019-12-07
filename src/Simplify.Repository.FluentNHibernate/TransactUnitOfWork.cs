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
		private ITransaction _transaction;

		/// <summary>
		/// Initializes a new instance of the <see cref="TransactUnitOfWork"/> class.
		/// </summary>
		/// <param name="sessionFactory">The session factory.</param>
		public TransactUnitOfWork(ISessionFactory sessionFactory) : base(sessionFactory)
		{
		}

		/// <summary>
		/// Begins the transaction.
		/// </summary>
		/// <param name="isolationLevel">The isolation level.</param>
		public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			_transaction = Session.BeginTransaction(isolationLevel);
		}

		/// <summary>
		/// Commits transaction.
		/// </summary>
		/// <exception cref="InvalidOperationException">Oops! We don't have an active transaction</exception>
		public virtual void Commit()
		{
			if (!_transaction.IsActive)
				throw new InvalidOperationException("Oops! We don't have an active transaction");

			_transaction.Commit();
		}

		/// <summary>
		/// Commits transaction asynchronously.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException">Oops! We don't have an active transaction</exception>
		public Task CommitAsync()
		{
			if (!_transaction.IsActive)
				throw new InvalidOperationException("Oops! We don't have an active transaction");

			return _transaction.CommitAsync();
		}

		/// <summary>
		/// Rollbacks transaction.
		/// </summary>
		public virtual void Rollback()
		{
			if (_transaction.IsActive)
				_transaction.Rollback();
		}

		/// <summary>
		/// Rollbacks transaction asynchronously.
		/// </summary>
		/// <returns></returns>
		public Task RollbackAsync()
		{
			return _transaction.IsActive ? _transaction.RollbackAsync() : Task.Delay(0);
		}
	}
}
using System.Data;
using System.Threading.Tasks;

namespace Simplify.Repository
{
	/// <summary>
	/// Represent unit of work with manual open transaction
	/// </summary>
	public interface ITransactUnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// Begins the transaction.
		/// </summary>
		/// <param name="isolationLevel">The isolation level.</param>
		void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

		/// <summary>
		/// Commits transaction.
		/// </summary>
		void Commit();

		/// <summary>
		/// Commits transaction asynchronously.
		/// </summary>
		/// <returns></returns>
		Task CommitAsync();

		/// <summary>
		/// Rollbacks transaction.
		/// </summary>
		void Rollback();

		/// <summary>
		/// Rollbacks transaction asynchronously.
		/// </summary>
		Task RollbackAsync();
	}
}
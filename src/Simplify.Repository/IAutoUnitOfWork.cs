using System;

namespace Simplify.Repository
{
	/// <summary>
	/// Represent unit of work with auto-open transaction
	/// </summary>
	[Obsolete("Obsolete in terms of architecture, please use ITransactUnitOfWork")]
	public interface IAutoUnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// Commits transaction.
		/// </summary>
		void Commit();

		/// <summary>
		/// Rollbacks transaction.
		/// </summary>
		void Rollback();
	}
}
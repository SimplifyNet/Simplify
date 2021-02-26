using Simplify.DI;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides Simplify.Repository.FluentNHibernate registrations
	/// </summary>
	public static class SimplifyDIRegistratorExtensions
	{
		/// <summary>
		/// Registers the scoped GenericRepository&lt;T&gt; and IGenericRepository&lt;T&gt; as TransactGenericRepository&lt;T&gt; transact repository.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TUnitOfWork">The type of the unit of work.</typeparam>
		/// <param name="registrator">The registrator.</param>
		/// <returns></returns>
		public static IDIRegistrator RegisterTransactRepository<T, TUnitOfWork>(this IDIRegistrator registrator)
			where T : class
			where TUnitOfWork : ITransactUnitOfWork
			=> registrator.Register<GenericRepository<T>>()
				.Register<IGenericRepository<T>>(r =>
					new TransactGenericRepository<T>(r.Resolve<GenericRepository<T>>(), r.Resolve<TUnitOfWork>()));

		/// <summary>
		/// Registers the scoped StatelessGenericRepository&lt;T&gt; and IGenericRepository&lt;T&gt; as TransactGenericRepository&lt;T&gt; transact repository.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TUnitOfWork">The type of the unit of work.</typeparam>
		/// <param name="registrator">The registrator.</param>
		/// <returns></returns>
		public static IDIRegistrator RegisterStatelessTransactRepository<T, TUnitOfWork>(this IDIRegistrator registrator)
			where T : class
			where TUnitOfWork : ITransactUnitOfWork
			=> registrator.Register<StatelessGenericRepository<T>>()
				.Register<IGenericRepository<T>>(r =>
					new TransactGenericRepository<T>(r.Resolve<StatelessGenericRepository<T>>(), r.Resolve<TUnitOfWork>()));
	}
}
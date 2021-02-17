using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NHibernate;
using Simplify.FluentNHibernate;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides generic repository pattern for easy NHibernate repositories implementation
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class GenericRepository<T> : IGenericRepository<T>
		where T : class
	{
		/// <summary>
		/// The NHibernate session
		/// </summary>
		protected readonly ISession Session;

		/// <summary>
		/// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
		/// </summary>
		/// <param name="session">The session.</param>
		public GenericRepository(ISession session) => Session = session;

		/// <summary>
		/// Gets the single object by identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public T GetSingleByID(object id) => Session.Get<T>(id);

		/// <summary>
		/// Gets the single object by identifier asynchronously.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public Task<T> GetSingleByIDAsync(object id) => Session.GetAsync<T>(id);

		/// <summary>
		/// Gets the single object by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public T GetSingleByQuery(Expression<Func<T, bool>> query) => Session.GetSingleObject(query);

		/// <summary>
		/// Gets the single object by query asynchronously.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public Task<T> GetSingleByQueryAsync(Expression<Func<T, bool>> query) => Session.GetSingleObjectAsync(query);

		/// <summary>
		/// Gets the first object by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public T GetFirstByQuery(Expression<Func<T, bool>> query) => Session.GetFirstObject(query);

		/// <summary>
		/// Gets the first object by query asynchronously.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public Task<T> GetFirstByQueryAsync(Expression<Func<T, bool>> query) => Session.GetFirstObjectAsync(query);

		/// <summary>
		/// Gets the multiple objects by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="customProcessing">The custom processing.</param>
		/// <returns></returns>
		public IList<T> GetMultipleByQuery(Expression<Func<T, bool>>? query = null, Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
			=> Session.GetList(query, customProcessing);

		/// <summary>
		/// Gets the multiple objects by query asynchronously.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="customProcessing">The custom processing.</param>
		/// <returns></returns>
		public Task<IList<T>> GetMultipleByQueryAsync(Expression<Func<T, bool>>? query = null, Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
			=> Session.GetListAsync(query, customProcessing);

		/// <summary>
		/// Gets the multiple paged elements list.
		/// </summary>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="itemsPerPage">The items per page number.</param>
		/// <param name="query">The query.</param>
		/// <param name="customProcessing">The custom processing.</param>
		/// <returns></returns>
		public IList<T> GetPaged(int pageIndex,
			int itemsPerPage,
			Expression<Func<T, bool>>? query = null,
			Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
			=> Session.GetListPaged(pageIndex, itemsPerPage, query, customProcessing);

		/// <summary>
		/// Gets the multiple paged elements list asynchronously.
		/// </summary>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="itemsPerPage">The items per page number.</param>
		/// <param name="query">The query.</param>
		/// <param name="customProcessing">The custom processing.</param>
		/// <returns></returns>
		public Task<IList<T>> GetPagedAsync(int pageIndex,
			int itemsPerPage,
			Expression<Func<T, bool>>? query = null,
			Func<IQueryable<T>, IQueryable<T>>? customProcessing = null) =>
			Session.GetListPagedAsync(pageIndex, itemsPerPage, query, customProcessing);

		/// <summary>
		/// Gets the number of elements.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public int GetCount(Expression<Func<T, bool>>? query = null) => Session.GetCount(query);

		/// <summary>
		/// Gets the number of elements asynchronously.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public Task<int> GetCountAsync(Expression<Func<T, bool>>? query = null) => Session.GetCountAsync(query);

		/// <summary>
		/// Gets the long number of elements.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public long GetLongCount(Expression<Func<T, bool>>? query = null) => Session.GetLongCount(query);

		/// <summary>
		/// Gets the long number of elements asynchronously.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public Task<long> GetLongCountAsync(Expression<Func<T, bool>>? query = null) => Session.GetLongCountAsync(query);

		/// <summary>
		/// Adds the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public object Add(T entity) => Session.Save(entity);

		/// <summary>
		/// Adds the object asynchronously.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// The generated identifier
		/// </returns>
		public Task<object> AddAsync(T entity) => Session.SaveAsync(entity);

		/// <summary>
		/// Deletes the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void Delete(T entity) => Session.Delete(entity);

		/// <summary>
		/// Deletes the object asynchronously.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		public Task DeleteAsync(T entity) => Session.DeleteAsync(entity);

		/// <summary>
		/// Updates the object.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void Update(T entity) => Session.Update(entity);

		/// <summary>
		/// Updates the object asynchronously.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		public Task UpdateAsync(T entity) => Session.UpdateAsync(entity);
	}
}
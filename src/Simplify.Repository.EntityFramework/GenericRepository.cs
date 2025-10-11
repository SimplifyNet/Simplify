using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Simplify.Repository.EntityFramework;

/// <summary>
/// Provides generic repository pattern for easy Entity Framework repositories implementation
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
/// </remarks>
/// <param name="session">The session.</param>
public class GenericRepository<T>(DbContext session) : IGenericRepository<T>
	where T : class
{
	/// <summary>
	/// The NHibernate session
	/// </summary>
	protected readonly DbContext Session = session;

	/// <summary>
	/// Gets the single object by identifier.
	/// </summary>
	/// <param name="id">The identifier.</param>
	/// <returns></returns>
	public T GetSingleByID(object id) => Session.Find<T>(id)!;

	/// <summary>
	/// Gets the single object by identifier asynchronously.
	/// </summary>
	/// <param name="id">The identifier.</param>
	/// <returns></returns>
	public Task<T> GetSingleByIDAsync(object id) => Session.FindAsync<T>(id).AsTask()!;

	/// <summary>
	/// Gets the single object by query.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public T GetSingleByQuery(Expression<Func<T, bool>> query) => Session.Set<T>().SingleOrDefault(query)!;

	/// <summary>
	/// Gets the single object by query asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public Task<T> GetSingleByQueryAsync(Expression<Func<T, bool>> query) => Session.Set<T>().FirstAsync(query);

	/// <summary>
	/// Gets the first object by query.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public T GetFirstByQuery(Expression<Func<T, bool>> query) => Session.Set<T>().FirstOrDefault(query)!;

	/// <summary>
	/// Gets the first object by query asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public Task<T> GetFirstByQueryAsync(Expression<Func<T, bool>> query) => Session.Set<T>().FirstOrDefaultAsync(query)!;

	/// <summary>
	/// Gets the multiple objects by query or all objects without query.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public IList<T> GetMultiple(Expression<Func<T, bool>>? query = null, Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
	{
		var queryable = Session.Set<T>().AsQueryable();

		if (query != null)
			queryable = queryable.Where(query);

		if (customProcessing != null)
			queryable = customProcessing(queryable);

		return queryable.ToList();
	}

	/// <summary>
	/// Gets the multiple objects by query or all objects without query asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public async Task<IList<T>> GetMultipleAsync(Expression<Func<T, bool>>? query = null, Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
	{
		var queryable = Session.Set<T>().AsQueryable();

		if (query != null)
			queryable = queryable.Where(query);

		if (customProcessing != null)
			queryable = customProcessing(queryable);

		return await queryable.ToListAsync();
	}

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
	{
		var queryable = Session.Set<T>().AsQueryable();

		if (query != null)
			queryable = queryable.Where(query);

		if (customProcessing != null)
			queryable = customProcessing(queryable);

		return queryable.Skip(pageIndex * itemsPerPage)
			.Take(itemsPerPage).ToList();
	}

	/// <summary>
	/// Gets the multiple paged elements list asynchronously.
	/// </summary>
	/// <param name="pageIndex">Index of the page.</param>
	/// <param name="itemsPerPage">The items per page number.</param>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public async Task<IList<T>> GetPagedAsync(int pageIndex,
		int itemsPerPage,
		Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
	{
		var queryable = Session.Set<T>().AsQueryable();

		if (query != null)
			queryable = queryable.Where(query);

		if (customProcessing != null)
			queryable = customProcessing(queryable);

		return await queryable.Skip(pageIndex * itemsPerPage)
			.Take(itemsPerPage).ToListAsync();
	}

	/// <summary>
	/// Gets the number of elements.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public int GetCount(Expression<Func<T, bool>>? query = null)
	{
		var queryable = Session.Set<T>().AsQueryable();

		if (query != null)
			queryable = queryable.Where(query);

		return queryable.Count();
	}

	/// <summary>
	/// Gets the number of elements asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public Task<int> GetCountAsync(Expression<Func<T, bool>>? query = null)
	{
		var queryable = Session.Set<T>().AsQueryable();

		if (query != null)
			queryable = queryable.Where(query);

		return queryable.CountAsync();
	}

	/// <summary>
	/// Gets the long number of elements.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public long GetLongCount(Expression<Func<T, bool>>? query = null)
	{
		var queryable = Session.Set<T>().AsQueryable();

		if (query != null)
			queryable = queryable.Where(query);

		return queryable.LongCount();
	}

	/// <summary>
	/// Gets the long number of elements asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public Task<long> GetLongCountAsync(Expression<Func<T, bool>>? query = null)
	{
		var queryable = Session.Set<T>().AsQueryable();

		if (query != null)
			queryable = queryable.Where(query);

		return queryable.LongCountAsync();
	}

	/// <summary>
	/// Adds the object.
	/// </summary>
	/// <param name="entity">The entity.</param>
	public object Add(T entity) => Session.Add(entity);

	/// <summary>
	/// Adds the object asynchronously.
	/// </summary>
	/// <param name="entity">The entity.</param>
	/// <returns>
	/// The generated identifier
	/// </returns>
	public async Task<object> AddAsync(T entity) => await Session.AddAsync(entity).AsTask();

	/// <summary>
	/// Deletes the object.
	/// </summary>
	/// <param name="entity">The entity.</param>
	public void Delete(T entity) => Session.Remove(entity);

	/// <summary>
	/// Deletes the object asynchronously.
	/// </summary>
	/// <param name="entity">The entity.</param>
	/// <returns></returns>
	public Task DeleteAsync(T entity)
	{
		Session.Remove(entity);

		return Task.CompletedTask;
	}

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
	public Task UpdateAsync(T entity)
	{
		Session.Update(entity);

		return Task.CompletedTask;
	}
}
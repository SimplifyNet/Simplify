using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace Simplify.FluentNHibernate;

/// <summary>
/// NHibernate.ISession extensions
/// </summary>
public static class SessionExtensions
{
	#region Single objects operations

	/// <summary>
	/// Get an object from database by filter (in case of several objects returned exception will be thrown)
	/// </summary>
	/// <typeparam name="T">The type of the object</typeparam>
	/// <param name="session">The NHibernate session.</param>
	/// <param name="query">Query</param>
	public static T GetSingleObject<T>(this ISession session, Expression<Func<T, bool>>? query = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		return queryable.SingleOrDefault()!;
	}

	/// <summary>
	/// Get an object from database by filter asynchronously (in case of several objects returned exception will be thrown)
	/// </summary>
	/// <typeparam name="T">The type of the object</typeparam>
	/// <param name="session">The NHibernate session.</param>
	/// <param name="query">Query</param>
	public static Task<T> GetSingleObjectAsync<T>(this ISession session, Expression<Func<T, bool>>? query = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		return queryable.SingleOrDefaultAsync();
	}

	/// <summary>
	/// Get and cache an object from database by filter (in case of several objects returned exception will be thrown)
	/// </summary>
	/// <typeparam name="T">The type of the object</typeparam>
	/// <param name="session">The NHibernate session.</param>
	/// <param name="query">Query</param>
	public static T GetSingleObjectCacheable<T>(this ISession session, Expression<Func<T, bool>>? query = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		queryable = queryable.WithOptions(x => x.SetCacheable(true));

		return queryable.SingleOrDefault()!;
	}

	/// <summary>
	/// Get and cache an object from database by filter asynchronously (in case of several objects returned exception will be thrown)
	/// </summary>
	/// <typeparam name="T">The type of the object</typeparam>
	/// <param name="session">The NHibernate session.</param>
	/// <param name="query">Query</param>
	public static Task<T> GetSingleObjectCacheableAsync<T>(this ISession session, Expression<Func<T, bool>>? query = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		queryable = queryable.WithOptions(x => x.SetCacheable(true));

		return queryable.SingleOrDefaultAsync();
	}

	/// <summary>
	/// Get a first object from database by filter
	/// </summary>
	/// <typeparam name="T">The type of the object</typeparam>
	/// <param name="session">The NHibernate session.</param>
	/// <param name="query">Query</param>
	/// <returns></returns>
	public static T GetFirstObject<T>(this ISession session, Expression<Func<T, bool>>? query = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		return queryable.FirstOrDefault()!;
	}

	/// <summary>
	/// Get a first object from database by filter asynchronously
	/// </summary>
	/// <typeparam name="T">The type of the object</typeparam>
	/// <param name="session">The NHibernate session.</param>
	/// <param name="query">Query</param>
	/// <returns></returns>
	public static Task<T> GetFirstObjectAsync<T>(this ISession session, Expression<Func<T, bool>>? query = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		return queryable.FirstOrDefaultAsync();
	}

	#endregion Single objects operations

	#region List operations

	/// <summary>
	/// Get a list of objects
	/// </summary>
	/// <typeparam name="T">The type of elements</typeparam>
	/// <param name="session">The NHibernate session.</param>
	/// <param name="query">Query</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns>
	/// List of objects
	/// </returns>
	public static IList<T> GetList<T>(this ISession session,
		Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		if (customProcessing != null)
			queryable = customProcessing(queryable);

		return queryable.ToList();
	}

	/// <summary>
	/// Get a list of objects asynchronously
	/// </summary>
	/// <typeparam name="T">The type of elements</typeparam>
	/// <param name="session">The NHibernate session.</param>
	/// <param name="query">Query</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns>
	/// List of objects
	/// </returns>
	public static async Task<IList<T>> GetListAsync<T>(this ISession session,
		Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		if (customProcessing != null)
			queryable = customProcessing(queryable);

		return await queryable.ToListAsync();
	}

	/// <summary>
	/// Gets the list of objects paged.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="session">The session.</param>
	/// <param name="pageIndex">Index of the page.</param>
	/// <param name="itemsPerPage">The items per page.</param>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public static IList<T> GetListPaged<T>(this ISession session,
		int pageIndex,
		int itemsPerPage,
		Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		if (customProcessing != null)
			queryable = customProcessing(queryable);

		return queryable.Skip(pageIndex * itemsPerPage)
			.Take(itemsPerPage).ToList();
	}

	/// <summary>
	/// Gets the list of objects paged asynchronously.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="session">The session.</param>
	/// <param name="pageIndex">Index of the page.</param>
	/// <param name="itemsPerPage">The items per page.</param>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public static async Task<IList<T>> GetListPagedAsync<T>(this ISession session,
		int pageIndex,
		int itemsPerPage,
		Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		if (customProcessing != null)
			queryable = customProcessing(queryable);

		return await queryable.Skip(pageIndex * itemsPerPage)
			.Take(itemsPerPage).ToListAsync();
	}

	#endregion List operations

	#region Count operations

	/// <summary>
	/// Gets the number of elements.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="session">The session.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public static int GetCount<T>(this ISession session, Expression<Func<T, bool>>? query = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		return queryable.Count();
	}

	/// <summary>
	/// Gets the number of elements asynchronously.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="session">The session.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public static Task<int> GetCountAsync<T>(this ISession session, Expression<Func<T, bool>>? query = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		return queryable.CountAsync();
	}

	/// <summary>
	/// Gets the number of elements.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="session">The session.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public static long GetLongCount<T>(this ISession session, Expression<Func<T, bool>>? query = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		return queryable.LongCount();
	}

	/// <summary>
	/// Gets the number of elements asynchronously.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="session">The session.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public static Task<long> GetLongCountAsync<T>(this ISession session, Expression<Func<T, bool>>? query = null)
		where T : class
	{
		var queryable = session.Query<T>();

		if (query != null)
			queryable = queryable.Where(query);

		return queryable.LongCountAsync();
	}

	#endregion Count operations
}
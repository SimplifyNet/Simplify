using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Simplify.Repository;

/// <summary>
/// Provides transactions wrapper to all repository methods
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="IGenericRepository{T}" />
/// <remarks>
/// Initializes a new instance of the <see cref="TransactGenericRepository{T}" /> class.
/// </remarks>
/// <param name="baseRepository">The base repository.</param>
/// <param name="unitOfWork">The unit of work.</param>
/// <param name="isolationLevel">The isolation level.</param>
public class TransactGenericRepository<T>(IGenericRepository<T> baseRepository, ITransactUnitOfWork unitOfWork, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) : IGenericRepository<T>
	where T : class
{

	/// <summary>
	/// Adds the object.
	/// </summary>
	/// <param name="entity">The entity.</param>
	/// <returns>
	/// The generated identifier
	/// </returns>
	public object Add(T entity) => Execute(() => baseRepository.Add(entity));

	/// <summary>
	/// Adds the object asynchronously.
	/// </summary>
	/// <param name="entity">The entity.</param>
	/// <returns>
	/// The generated identifier
	/// </returns>
	public Task<object> AddAsync(T entity) => ExecuteAsync(() => baseRepository.AddAsync(entity));

	/// <summary>
	/// Deletes the object.
	/// </summary>
	/// <param name="entity">The entity.</param>
	public void Delete(T entity) => Execute(() => baseRepository.Delete(entity));

	/// <summary>
	/// Deletes the object asynchronously.
	/// </summary>
	/// <param name="entity">The entity.</param>
	public Task DeleteAsync(T entity) => ExecuteAsync(() => baseRepository.DeleteAsync(entity));

	/// <summary>
	/// Gets the number of elements.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public int GetCount(Expression<Func<T, bool>>? query = null) => Execute(() => baseRepository.GetCount(query));

	/// <summary>
	/// Gets the number of elements asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public Task<int> GetCountAsync(Expression<Func<T, bool>>? query = null) => ExecuteAsync(() => baseRepository.GetCountAsync(query));

	/// <summary>
	/// Gets the first object by query.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public T GetFirstByQuery(Expression<Func<T, bool>> query) => Execute(() => baseRepository.GetFirstByQuery(query));

	/// <summary>
	/// Gets the first object by query asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public Task<T> GetFirstByQueryAsync(Expression<Func<T, bool>> query) => ExecuteAsync(() => baseRepository.GetFirstByQueryAsync(query));

	/// <summary>
	/// Gets the long number of elements.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public long GetLongCount(Expression<Func<T, bool>>? query = null) => Execute(() => baseRepository.GetLongCount(query));

	/// <summary>
	/// Gets the long number of elements asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public Task<long> GetLongCountAsync(Expression<Func<T, bool>>? query = null) => ExecuteAsync(() => baseRepository.GetLongCountAsync(query));

	/// <summary>
	/// Gets the multiple objects by query or all objects without query.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public IList<T> GetMultiple(Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null) =>
		Execute(() => baseRepository.GetMultiple(query, customProcessing));

	/// <summary>
	/// Gets the multiple objects by query or all objects without query asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public Task<IList<T>> GetMultipleAsync(Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null) =>
		ExecuteAsync(() => baseRepository.GetMultipleAsync(query, customProcessing));

	/// <summary>
	/// Gets the multiple paged elements list.
	/// </summary>
	/// <param name="pageIndex">Index of the page.</param>
	/// <param name="itemsPerPage">The items per page number.</param>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public IList<T> GetPaged(int pageIndex, int itemsPerPage, Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null) =>
		Execute(() => baseRepository.GetPaged(pageIndex, itemsPerPage, query, customProcessing));

	/// <summary>
	/// Gets the multiple paged elements list asynchronously.
	/// </summary>
	/// <param name="pageIndex">Index of the page.</param>
	/// <param name="itemsPerPage">The items per page number.</param>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public Task<IList<T>> GetPagedAsync(int pageIndex, int itemsPerPage, Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null) =>
		ExecuteAsync(() => baseRepository.GetPagedAsync(pageIndex, itemsPerPage, query, customProcessing));

	/// <summary>
	/// Gets the single object by identifier.
	/// </summary>
	/// <param name="id">The identifier.</param>
	/// <returns></returns>
	public T GetSingleByID(object id) => Execute(() => baseRepository.GetSingleByID(id));

	/// <summary>
	/// Gets the single object by identifier asynchronously.
	/// </summary>
	/// <param name="id">The identifier.</param>
	/// <returns></returns>
	public Task<T> GetSingleByIDAsync(object id) => ExecuteAsync(() => baseRepository.GetSingleByIDAsync(id));

	/// <summary>
	/// Gets the single object by query.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public T GetSingleByQuery(Expression<Func<T, bool>> query) => Execute(() => baseRepository.GetSingleByQuery(query));

	/// <summary>
	/// Gets the single object by query asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public Task<T> GetSingleByQueryAsync(Expression<Func<T, bool>> query) => ExecuteAsync(() => baseRepository.GetSingleByQueryAsync(query));

	/// <summary>
	/// Updates the object.
	/// </summary>
	/// <param name="entity">The entity.</param>
	public void Update(T entity) => Execute(() => baseRepository.Update(entity));

	/// <summary>
	/// Updates the object asynchronously.
	/// </summary>
	/// <param name="entity">The entity.</param>
	public Task UpdateAsync(T entity) => ExecuteAsync(() => baseRepository.UpdateAsync(entity));

	private TResult Execute<TResult>(Func<TResult> operation)
	{
		if (unitOfWork.IsTransactionActive)
			return operation();

		unitOfWork.BeginTransaction(isolationLevel);

		try
		{
			var result = operation();

			unitOfWork.Commit();

			return result;
		}
		catch
		{
			unitOfWork.Rollback();

			throw;
		}
	}

	private void Execute(Action operation) =>
		Execute(() =>
		{
			operation();

			return true;
		});

	private async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation)
	{
		if (unitOfWork.IsTransactionActive)
			return await operation();

		unitOfWork.BeginTransaction(isolationLevel);

		try
		{
			var result = await operation();

			await unitOfWork.CommitAsync();

			return result;
		}
		catch
		{
			await unitOfWork.RollbackAsync();

			throw;
		}
	}

	private async Task ExecuteAsync(Func<Task> operation) =>
		await ExecuteAsync(async () =>
		{
			await operation();

			return true;
		});
}

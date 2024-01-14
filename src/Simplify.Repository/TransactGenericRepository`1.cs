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
public class TransactGenericRepository<T> : IGenericRepository<T>
	where T : class
{
	private readonly IGenericRepository<T> _baseRepository;
	private readonly IsolationLevel _isolationLevel;
	private readonly ITransactUnitOfWork _unitOfWork;

	/// <summary>
	/// Initializes a new instance of the <see cref="TransactGenericRepository{T}" /> class.
	/// </summary>
	/// <param name="baseRepository">The base repository.</param>
	/// <param name="unitOfWork">The unit of work.</param>
	/// <param name="isolationLevel">The isolation level.</param>
	public TransactGenericRepository(IGenericRepository<T> baseRepository, ITransactUnitOfWork unitOfWork, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
	{
		_baseRepository = baseRepository;
		_unitOfWork = unitOfWork;
		_isolationLevel = isolationLevel;
	}

	/// <summary>
	/// Adds the object.
	/// </summary>
	/// <param name="entity">The entity.</param>
	/// <returns>
	/// The generated identifier
	/// </returns>
	public object Add(T entity)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = _baseRepository.Add(entity);

		if (!skip)
			_unitOfWork.Commit();

		return result;
	}

	/// <summary>
	/// Adds the object asynchronously.
	/// </summary>
	/// <param name="entity">The entity.</param>
	/// <returns>
	/// The generated identifier
	/// </returns>
	public async Task<object> AddAsync(T entity)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = await _baseRepository.AddAsync(entity);

		if (!skip)
			await _unitOfWork.CommitAsync();

		return result;
	}

	/// <summary>
	/// Deletes the object.
	/// </summary>
	/// <param name="entity">The entity.</param>
	public void Delete(T entity)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		_baseRepository.Delete(entity);

		if (!skip)
			_unitOfWork.Commit();
	}

	/// <summary>
	/// Deletes the object asynchronously.
	/// </summary>
	/// <param name="entity">The entity.</param>
	public async Task DeleteAsync(T entity)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		await _baseRepository.DeleteAsync(entity);

		if (!skip)
			await _unitOfWork.CommitAsync();
	}

	/// <summary>
	/// Gets the number of elements.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public int GetCount(Expression<Func<T, bool>>? query = null)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = _baseRepository.GetCount(query);

		if (!skip)
			_unitOfWork.Commit();

		return result;
	}

	/// <summary>
	/// Gets the number of elements asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public async Task<int> GetCountAsync(Expression<Func<T, bool>>? query = null)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = await _baseRepository.GetCountAsync(query);

		if (!skip)
			await _unitOfWork.CommitAsync();

		return result;
	}

	/// <summary>
	/// Gets the first object by query.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public T GetFirstByQuery(Expression<Func<T, bool>> query)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = _baseRepository.GetFirstByQuery(query);

		if (!skip)
			_unitOfWork.Commit();

		return result;
	}

	/// <summary>
	/// Gets the first object by query asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public async Task<T> GetFirstByQueryAsync(Expression<Func<T, bool>> query)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = await _baseRepository.GetFirstByQueryAsync(query);

		if (!skip)
			await _unitOfWork.CommitAsync();

		return result;
	}

	/// <summary>
	/// Gets the long number of elements.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public long GetLongCount(Expression<Func<T, bool>>? query = null)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = _baseRepository.GetLongCount(query);

		if (!skip)
			_unitOfWork.Commit();

		return result;
	}

	/// <summary>
	/// Gets the long number of elements asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public async Task<long> GetLongCountAsync(Expression<Func<T, bool>>? query = null)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = await _baseRepository.GetLongCountAsync(query);

		if (!skip)
			await _unitOfWork.CommitAsync();

		return result;
	}

	/// <summary>
	/// Gets the multiple objects by query or all objects without query.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public IList<T> GetMultiple(Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = _baseRepository.GetMultiple(query, customProcessing);

		if (!skip)
			_unitOfWork.Commit();

		return result;
	}

	/// <summary>
	/// Gets the multiple objects by query or all objects without query asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public async Task<IList<T>> GetMultipleAsync(Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = await _baseRepository.GetMultipleAsync(query, customProcessing);

		if (!skip)
			await _unitOfWork.CommitAsync();

		return result;
	}

	/// <summary>
	/// Gets the multiple paged elements list.
	/// </summary>
	/// <param name="pageIndex">Index of the page.</param>
	/// <param name="itemsPerPage">The items per page number.</param>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public IList<T> GetPaged(int pageIndex, int itemsPerPage, Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = _baseRepository.GetPaged(pageIndex, itemsPerPage, query, customProcessing);

		if (!skip)
			_unitOfWork.Commit();

		return result;
	}

	/// <summary>
	/// Gets the multiple paged elements list asynchronously.
	/// </summary>
	/// <param name="pageIndex">Index of the page.</param>
	/// <param name="itemsPerPage">The items per page number.</param>
	/// <param name="query">The query.</param>
	/// <param name="customProcessing">The custom processing.</param>
	/// <returns></returns>
	public async Task<IList<T>> GetPagedAsync(int pageIndex, int itemsPerPage, Expression<Func<T, bool>>? query = null,
		Func<IQueryable<T>, IQueryable<T>>? customProcessing = null)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = await _baseRepository.GetPagedAsync(pageIndex, itemsPerPage, query, customProcessing);

		if (!skip)
			await _unitOfWork.CommitAsync();

		return result;
	}

	/// <summary>
	/// Gets the single object by identifier.
	/// </summary>
	/// <param name="id">The identifier.</param>
	/// <returns></returns>
	public T GetSingleByID(object id)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = _baseRepository.GetSingleByID(id);

		if (!skip)
			_unitOfWork.Commit();

		return result;
	}

	/// <summary>
	/// Gets the single object by identifier asynchronously.
	/// </summary>
	/// <param name="id">The identifier.</param>
	/// <returns></returns>
	public async Task<T> GetSingleByIDAsync(object id)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = await _baseRepository.GetSingleByIDAsync(id);

		if (!skip)
			await _unitOfWork.CommitAsync();

		return result;
	}

	/// <summary>
	/// Gets the single object by query.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public T GetSingleByQuery(Expression<Func<T, bool>> query)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = _baseRepository.GetSingleByQuery(query);

		if (!skip)
			_unitOfWork.Commit();

		return result;
	}

	/// <summary>
	/// Gets the single object by query asynchronously.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	public async Task<T> GetSingleByQueryAsync(Expression<Func<T, bool>> query)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		var result = await _baseRepository.GetSingleByQueryAsync(query);

		if (!skip)
			await _unitOfWork.CommitAsync();

		return result;
	}

	/// <summary>
	/// Updates the object.
	/// </summary>
	/// <param name="entity">The entity.</param>
	public void Update(T entity)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		_baseRepository.Update(entity);

		if (!skip)
			_unitOfWork.Commit();
	}

	/// <summary>
	/// Updates the object asynchronously.
	/// </summary>
	/// <param name="entity">The entity.</param>
	public async Task UpdateAsync(T entity)
	{
		var skip = false;

		if (_unitOfWork.IsTransactionActive)
			skip = true;
		else
			_unitOfWork.BeginTransaction(_isolationLevel);

		await _baseRepository.UpdateAsync(entity);

		if (!skip)
			await _unitOfWork.CommitAsync();
	}
}
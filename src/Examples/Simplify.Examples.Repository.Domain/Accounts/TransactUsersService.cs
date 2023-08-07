using System.Data;
using System.Threading.Tasks;
using Simplify.Examples.Repository.Domain.Location;

namespace Simplify.Examples.Repository.Domain.Accounts;

public class TransactUsersService : IUsersService
{
	private readonly IUsersService _baseService;
	private readonly IExampleUnitOfWork _unitOfWork;

	public TransactUsersService(IUsersService baseService, IExampleUnitOfWork unitOfWork)
	{
		_baseService = baseService;
		_unitOfWork = unitOfWork;
	}

	public async Task<int> CreateUserAsync(IUser user)
	{
		_unitOfWork.BeginTransaction();

		var result = await _baseService.CreateUserAsync(user);

		await _unitOfWork.CommitAsync();

		return result;
	}

	public IUser GetUser(string userName)
	{
		_unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted);

		var item = _baseService.GetUser(userName);

		_unitOfWork.Commit();

		return item;
	}

	public void SetUserCity(IUser user, ICity city)
	{
		_unitOfWork.BeginTransaction();

		_baseService.SetUserCity(user, city);

		_unitOfWork.Commit();
	}
}
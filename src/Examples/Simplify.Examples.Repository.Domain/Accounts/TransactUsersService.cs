using System.Data;
using System.Threading.Tasks;
using Simplify.Examples.Repository.Domain.Location;

namespace Simplify.Examples.Repository.Domain.Accounts;

public class TransactUsersService(IUsersService baseService, IExampleUnitOfWork unitOfWork) : IUsersService
{
	public async Task<int> CreateUserAsync(IUser user)
	{
		unitOfWork.BeginTransaction();

		var result = await baseService.CreateUserAsync(user);

		await unitOfWork.CommitAsync();

		return result;
	}

	public IUser GetUser(string userName)
	{
		unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted);

		var item = baseService.GetUser(userName);

		unitOfWork.Commit();

		return item;
	}

	public void SetUserCity(IUser user, ICity city)
	{
		unitOfWork.BeginTransaction();

		baseService.SetUserCity(user, city);

		unitOfWork.Commit();
	}
}
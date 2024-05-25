using System;
using System.Threading.Tasks;
using Simplify.Examples.Repository.Domain.Location;
using Simplify.Repository;

namespace Simplify.Examples.Repository.Domain.Accounts;

public class UsersService(IGenericRepository<IUser> repository) : IUsersService
{
	public async Task<int> CreateUserAsync(IUser user)
	{
		var result = await repository.AddAsync(user);

		return (int)result;
	}

	public IUser GetUser(string userName)
	{
		ArgumentNullException.ThrowIfNull(userName);

		return repository.GetSingleByQuery(x => x.Name == userName);
	}

	public void SetUserCity(IUser user, ICity city)
	{
		ArgumentNullException.ThrowIfNull(user);

		user.City = city ?? throw new ArgumentNullException(nameof(city));

		repository.Update(user);
	}
}
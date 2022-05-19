using System.Collections.Generic;
using System.Threading.Tasks;
using Simplify.Bus.Impl.Tests.Domain.Users;

namespace Simplify.Bus.Impl.Tests.Database.Users;

public class UsersRepository : IUsersRepository
{
	public IList<IUser> DataStore { get; } = new List<IUser>();

	public Task Create(IUser user)
	{
		DataStore.Add(user);

		return Task.CompletedTask;
	}
}
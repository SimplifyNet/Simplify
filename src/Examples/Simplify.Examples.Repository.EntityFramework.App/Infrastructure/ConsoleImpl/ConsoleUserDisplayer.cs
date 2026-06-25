using System;
using Simplify.Examples.Repository.Domain.Accounts;

namespace Simplify.Examples.Repository.EntityFramework.App.Infrastructure.ConsoleImpl;

public class ConsoleUserDisplayer(IUsersService service) : IUserDisplayer
{
	public void DisplayUserInfo(string userName)
	{
		var user = service.GetUser(userName);

		if (user == null)
		{
			DisplayNotFoundMessage(userName);

			return;
		}

		DisplayUserToConsole(user);
	}

	private static void DisplayNotFoundMessage(string userName)
	{
		Console.WriteLine($"User with the name '{userName}' is not found");
	}

	private static void DisplayUserToConsole(IUser user)
	{
		Console.WriteLine($"User name: {user.Name}");
		Console.WriteLine($"User email: {user.EMail}");
		Console.WriteLine($"User city: {user.City?.LocalizableName}");
	}
}
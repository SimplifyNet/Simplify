namespace Simplify.Examples.Repository.EntityFramework.App.Infrastructure;

public class ArgsHandler(IUserDisplayer userDisplayer, ArgsVerifier argsVerifier)
{
	public void ProcessArgs(string[] args)
	{
		if (!argsVerifier.Verify(args))
			return;

		userDisplayer.DisplayUserInfo(args[0]);
	}
}
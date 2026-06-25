namespace Simplify.Examples.Repository.FluentNHibernate.App.Infrastructure;

public class ArgsHandler(IUserDisplayer userDisplayer, ArgsVerifier argsVerifier)
{
	public void ProcessArgs(string[] args)
	{
		if (!argsVerifier.Verify(args))
			return;

		userDisplayer.DisplayUserInfo(args[0]);
	}
}
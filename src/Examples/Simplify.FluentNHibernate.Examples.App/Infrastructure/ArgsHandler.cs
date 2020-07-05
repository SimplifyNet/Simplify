namespace Simplify.FluentNHibernate.Examples.App.Infrastructure
{
	public class ArgsHandler
	{
		private readonly IUserDisplayer _userDisplayer;
		private readonly ArgsVerifier _argsVerifier;

		public ArgsHandler(IUserDisplayer userDisplayer, ArgsVerifier argsVerifier)
		{
			_userDisplayer = userDisplayer;
			_argsVerifier = argsVerifier;
		}

		public void ProcessArgs(string[] args)
		{
			if (!_argsVerifier.Verify(args))
				return;

			_userDisplayer.DisplayUserInfo(args[0]);
		}
	}
}
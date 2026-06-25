namespace Simplify.Examples.Repository.FluentNHibernate.App.Infrastructure;

public class ArgsVerifier(INotifier notifier)
{
	public bool Verify(string[] args)
	{
		if (args.Length == 0)
		{
			notifier.ShowNoArgsMessage();

			return false;
		}

		if (args.Length > 1)
		{
			notifier.ShowTooManyArgsMessage();

			return false;
		}

		return true;
	}
}
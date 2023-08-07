namespace Simplify.Examples.Repository.FluentNHibernate.App.Infrastructure;

public class ArgsVerifier
{
	private readonly INotifier _notifier;

	public ArgsVerifier(INotifier notifier)
	{
		_notifier = notifier;
	}

	public bool Verify(string[] args)
	{
		if (args.Length == 0)
		{
			_notifier.ShowNoArgsMessage();

			return false;
		}

		if (args.Length > 1)
		{
			_notifier.ShowTooManyArgsMessage();

			return false;
		}

		return true;
	}
}
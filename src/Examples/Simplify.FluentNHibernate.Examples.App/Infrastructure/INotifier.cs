namespace Simplify.FluentNHibernate.Examples.App.Infrastructure
{
	public interface INotifier
	{
		void ShowNoArgsMessage();

		void ShowTooManyArgsMessage();
	}
}
namespace Simplify.Examples.Repository.FluentNHibernate.App.Infrastructure
{
	public interface INotifier
	{
		void ShowNoArgsMessage();

		void ShowTooManyArgsMessage();
	}
}
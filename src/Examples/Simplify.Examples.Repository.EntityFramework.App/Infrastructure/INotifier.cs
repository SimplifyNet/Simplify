namespace Simplify.Examples.Repository.EntityFramework.App.Infrastructure
{
	public interface INotifier
	{
		void ShowNoArgsMessage();

		void ShowTooManyArgsMessage();
	}
}
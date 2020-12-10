using Simplify.DI;

namespace Simplify.FluentNHibernate.Examples.App.Setup
{
	public static class IocRegistrations
	{
		public static IDIContainerProvider Register()
		{
			DIContainer.Current.RegisterConfiguration()
				.RegisterDatabase()
				.RegisterInfrastructure();

			return DIContainer.Current;
		}
	}
}
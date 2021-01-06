using Simplify.DI;

namespace Simplify.FluentNHibernate.Examples.App.Setup
{
	public static class IocRegistrations
	{
		public static IDIContainerProvider RegisterSimplifyFluentNHibernateExamplesApp(this IDIContainerProvider provider)
		{
			provider.RegisterConfiguration()
				.RegisterDatabase()
				.RegisterInfrastructure();

			return provider;
		}
	}
}
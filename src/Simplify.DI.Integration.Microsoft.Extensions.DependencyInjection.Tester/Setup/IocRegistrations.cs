namespace Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection.Tester.Setup;

public static class IocRegistrations
{
	public static IDIContainerProvider RegisterAll(this IDIContainerProvider provider)
	{
		provider.Register<Dependency>();

		return provider;
	}
}
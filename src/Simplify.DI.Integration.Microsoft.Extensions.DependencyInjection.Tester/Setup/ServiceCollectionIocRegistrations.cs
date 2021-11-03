using Microsoft.Extensions.DependencyInjection;

namespace Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection.Tester.Setup
{
	public static class ServiceCollectionIocRegistrations
	{
		public static IServiceCollection RegisterAll(this IServiceCollection services) =>
			services.AddScoped<Dependency2>();
	}
}
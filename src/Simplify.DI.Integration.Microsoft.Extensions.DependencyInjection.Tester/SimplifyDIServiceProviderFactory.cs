using System;
using Microsoft.Extensions.DependencyInjection;

namespace Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection.Tester;

/// <summary>
/// Plugs Simplify.DI into the .NET generic host as a custom container.
/// This is the modern, supported replacement for the deprecated WebHost/Startup pattern where
/// <c>Startup.ConfigureServices</c> returned an <see cref="IServiceProvider"/> (rejected by the generic host).
/// </summary>
public class SimplifyDIServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
{
	public IServiceCollection CreateBuilder(IServiceCollection services) => services;

	public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder) =>
		DIContainer.Current.IntegrateWithMicrosoftDependencyInjectionAndVerify(containerBuilder);
}

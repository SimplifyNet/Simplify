using System;
using Microsoft.Extensions.DependencyInjection;

namespace Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides IServiceProviderFactory implementation for Simplify.DI integration with Microsoft.Extensions.DependencyInjection hosting model
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="DIServiceProviderFactory"/>.
/// </remarks>
/// <param name="provider">The DI container provider.</param>
public class DIServiceProviderFactory(IDIContainerProvider provider) : IServiceProviderFactory<IDIContainerProvider>
{
	private readonly IDIContainerProvider _provider = provider ?? throw new ArgumentNullException(nameof(provider));

	/// <inheritdoc/>
	public IDIContainerProvider CreateBuilder(IServiceCollection services)
	{
		_provider.RegisterFromServiceCollection(services);

		return _provider;
	}

	/// <inheritdoc/>
	public IServiceProvider CreateServiceProvider(IDIContainerProvider containerBuilder)
	{
		var serviceProvider = containerBuilder.CreateServiceProvider();

		containerBuilder.Verify();

		return serviceProvider;
	}
}

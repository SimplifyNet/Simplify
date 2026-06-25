using System;
using Microsoft.Extensions.DependencyInjection;
using Simplify.DI.Provider.DryIoc;

namespace Simplify.DI.Provider.Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Providers Microsoft.DependencyInjection resolver
/// </summary>
/// <seealso cref="IDIResolver" />
/// <param name="serviceProvider">The resolver service provider.</param>
public class MicrosoftDependencyInjectionDIResolver(IServiceProvider serviceProvider) : IDIResolver
{
	/// <summary>
	/// Resolves the specified type.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns></returns>
	public object Resolve(Type type)
	{
		return serviceProvider.GetRequiredService(type);
	}
}
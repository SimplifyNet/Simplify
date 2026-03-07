using System;

namespace Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Simplify.DI based service provider for Microsoft.Extensions.DependencyInjection
/// </summary>
/// <seealso cref="IServiceProvider" />
/// <remarks>
/// Initializes a new instance of the <see cref="DIServiceProvider"/> class.
/// </remarks>
/// <param name="resolver">The registrator.</param>
public class DIServiceProvider(IDIResolver resolver) : IServiceProvider
{
	/// <summary>
	/// Gets the service object of the specified type.
	/// </summary>
	/// <param name="serviceType">An object that specifies the type of service object to get.</param>
	/// <returns>
	/// A service object of type <paramref name="serviceType">serviceType</paramref>.   -or-  null if there is no service object of type <paramref name="serviceType">serviceType</paramref>.
	/// </returns>
	public object GetService(Type serviceType) => resolver.Resolve(serviceType);
}
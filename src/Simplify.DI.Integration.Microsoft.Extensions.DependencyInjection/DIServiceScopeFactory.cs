using Microsoft.Extensions.DependencyInjection;

namespace Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Simplify.DI based service scope factory for Microsoft.Extensions.DependencyInjection
/// </summary>
/// <seealso cref="IServiceScopeFactory" />
/// <remarks>
/// Initializes a new instance of the <see cref="DIServiceScopeFactory"/> class.
/// </remarks>
/// <param name="contextHandler">The context handler.</param>
public class DIServiceScopeFactory(IDIContextHandler contextHandler) : IServiceScopeFactory
{
	/// <summary>
	/// Create an <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceScope" /> which
	/// contains an <see cref="T:System.IServiceProvider" /> used to resolve dependencies from a
	/// newly created scope.
	/// </summary>
	/// <returns>
	/// An <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceScope" /> controlling the
	/// lifetime of the scope. Once this is disposed, any scoped services that have been resolved
	/// from the <see cref="P:Microsoft.Extensions.DependencyInjection.IServiceScope.ServiceProvider" />
	/// will also be disposed.
	/// </returns>
	public IServiceScope CreateScope() => new DIServiceScope(contextHandler);
}
using System;
using DryIoc;

namespace Simplify.DI.Provider.DryIoc;

/// <summary>
/// Providers DryIoc resolver
/// </summary>
/// <seealso cref="IDIResolver" />
/// <param name="resolverContext">The resolver context.</param>
public class DryIocDIResolver(IResolverContext resolverContext) : IDIResolver
{
	/// <summary>
	/// Resolves the specified type.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns></returns>
	public object Resolve(Type type) => resolverContext.Resolve(type);
}
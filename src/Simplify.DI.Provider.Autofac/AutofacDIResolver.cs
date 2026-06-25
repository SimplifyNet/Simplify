using System;
using Autofac;

namespace Simplify.DI.Provider.Autofac;

/// <summary>
/// Providers Autofac resolver
/// </summary>
/// <seealso cref="IDIResolver" />
/// <param name="componentContext">The component context.</param>
public class AutofacDIResolver(IComponentContext componentContext) : IDIResolver
{
	/// <summary>
	/// Resolves the specified type.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns></returns>
	public object Resolve(Type type) => componentContext.Resolve(type);
}
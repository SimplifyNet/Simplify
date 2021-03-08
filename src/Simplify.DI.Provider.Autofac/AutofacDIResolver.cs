using System;
using Autofac;

namespace Simplify.DI.Provider.Autofac
{
	/// <summary>
	/// Providers Autofac resolver
	/// </summary>
	/// <seealso cref="IDIResolver" />
	public class AutofacDIResolver : IDIResolver
	{
		private readonly IComponentContext _componentContext;

		/// <summary>
		/// Initializes a new instance of the <see cref="AutofacDIResolver"/> class.
		/// </summary>
		/// <param name="componentContext">The component context.</param>
		public AutofacDIResolver(IComponentContext componentContext) => _componentContext = componentContext;

		/// <summary>
		/// Resolves the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public object Resolve(Type type) => _componentContext.Resolve(type);
	}
}
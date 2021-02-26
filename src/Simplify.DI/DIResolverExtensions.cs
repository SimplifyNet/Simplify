﻿namespace Simplify.DI
{
	/// <summary>
	/// Provides DI resolver extensions
	/// </summary>
	public static class DIResolverExtensions
	{
		/// <summary>
		/// Resolves the specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="resolver">The DI resolver.</param>
		/// <returns></returns>
		public static T Resolve<T>(this IDIResolver resolver) => (T)resolver.Resolve(typeof(T));
	}
}
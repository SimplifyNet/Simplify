using System;

namespace Simplify.System.Converters;

/// <summary>
/// Defines interface for simple object-to-object converter
/// </summary>
/// <typeparam name="TSource">Source type</typeparam>
/// <typeparam name="TDestination">Destination type</typeparam>
public interface IObjectConverter<in TSource, out TDestination>
{
	/// <summary>
	/// Converts source object to destination object
	/// </summary>
	/// <param name="source">Source object</param>
	/// <returns>Destination object</returns>
	TDestination Convert(TSource source);

	/// <summary>
	/// Provides Convert method as Func delegate
	/// </summary>
	/// <returns>Func delegate</returns>
	Func<TSource, TDestination> AsFunc();
}
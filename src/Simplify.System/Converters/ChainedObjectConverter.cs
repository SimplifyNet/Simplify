using System;

namespace Simplify.System.Converters;

/// <summary>
/// Provides customizable object-to-object converter that can be chained with similar converters
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TDestination"></typeparam>
public class ChainedObjectConverter<TSource, TDestination> : ObjectConverter<TSource, TDestination>
	where TDestination : TSource
{
	/// <summary>
	/// Func delegate that converts source to destination before the main converter delegate
	/// </summary>
	protected readonly Func<TSource, TDestination>? PreConvertFunc;

	/// <summary>
	/// Creates instance of ChainedObjectConverter
	/// </summary>
	/// <param name="convertFunc">Func delegate that converts source to destination</param>
	/// <param name="preConvertFunc">Func delegate that converts source to destination before the main converter delegate</param>
	public ChainedObjectConverter(Func<TSource, TDestination> convertFunc, Func<TSource, TDestination>? preConvertFunc = null)
		: base(convertFunc)
	{
		PreConvertFunc = preConvertFunc;
	}

	/// <summary>
	/// Creates instance of ChainedObjectConverter with uninitialized ConvertFunc
	/// </summary>
	/// <param name="preConvertFunc"></param>
	protected ChainedObjectConverter(Func<TSource, TDestination>? preConvertFunc = null)
	{
		PreConvertFunc = preConvertFunc;
	}

	/// <summary>
	/// Converts source object to destination object
	/// </summary>
	/// <param name="source">Source object</param>
	/// <returns>Destination object</returns>
	public override TDestination Convert(TSource source)
	{
		return PreConvertFunc is null
			? base.Convert(source)
			: base.Convert(PreConvertFunc(source));
	}
}
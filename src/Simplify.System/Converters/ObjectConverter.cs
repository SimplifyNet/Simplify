using System;

namespace Simplify.System.Converters
{
	/// <summary>
	/// Provides customizable object-to-object converter
	/// </summary>
	/// <typeparam name="TSource"></typeparam>
	/// <typeparam name="TDestination"></typeparam>
	public class ObjectConverter<TSource, TDestination> : IObjectConverter<TSource, TDestination>
	{
		/// <summary>
		/// Func delegate that converts source to destination
		/// </summary>
		protected readonly Func<TSource?, TDestination?> ConvertFunc;

		/// <summary>
		/// Creates instance of ObjectConverter
		/// </summary>
		/// <param name="convertFunc">Func delegate that converts source to destination</param>
		public ObjectConverter(Func<TSource?, TDestination?> convertFunc)
		{
			ConvertFunc = convertFunc ?? throw new ArgumentNullException(nameof(convertFunc), "Convert delegate cannot be null");
		}

		/// <summary>
		/// Implicitly provides Convert method as Func delegate
		/// </summary>
		/// <param name="converter"></param>
		public static implicit operator Func<TSource?, TDestination?>(ObjectConverter<TSource, TDestination> converter)
		{
			return converter.AsFunc();
		}

		/// <summary>
		/// Converts source object to destination object
		/// </summary>
		/// <param name="source">Source object</param>
		/// <returns>Destination object</returns>
		public virtual TDestination? Convert(TSource? source)
		{
			return ConvertFunc(source);
		}

		/// <summary>
		/// Provides Convert method as Func delegate
		/// </summary>
		/// <returns>Func delegate</returns>
		public virtual Func<TSource?, TDestination?> AsFunc()
		{
			return Convert;
		}
	}
}
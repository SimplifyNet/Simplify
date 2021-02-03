using System;
using System.Linq.Expressions;
using AutoMapper;

namespace Simplify.AutoMapper.Extensions
{
	/// <summary>
	/// Provides extensions for the IMappingExpressionExtensions
	/// </summary>
	public static class IMappingExpressionExtensions
	{
		/// <summary>
		/// Map destination member using a source member expression
		/// </summary>
		/// <typeparam name="TSource">Source type</typeparam>
		/// <typeparam name="TDest">Destination type</typeparam>
		/// <param name="mappingExpression">IMappingExpression</param>
		/// <param name="destinationMember">Destination member</param>
		/// <param name="sourceMember">Source member</param>
		/// <param name="memberOptions">Member options</param>
		/// <returns>Itself</returns>
		public static IMappingExpression<TSource, TDest> Map<TSource, TDest>(this IMappingExpression<TSource, TDest> mappingExpression,
			Expression<Func<TDest, object?>>? destinationMember,
			Expression<Func<TSource, object?>>? sourceMember = null,
			Action<IMemberConfigurationExpression<TSource, TDest, object?>>? memberOptions = null)
		{
			if (destinationMember is null)
				return mappingExpression;

			mappingExpression.ForMember(destinationMember, o =>
			{
				o.MapFrom(sourceMember ?? (x => x));
				memberOptions?.Invoke(o);
			});
			return mappingExpression;
		}

		/// <summary>
		/// Map destination member using a source member's path
		/// </summary>
		/// <typeparam name="TSource">Source type</typeparam>
		/// <typeparam name="TDest">Destination type</typeparam>
		/// <param name="mappingExpression">IMappingExpression</param>
		/// <param name="destinationMember">Destination member</param>
		/// <param name="sourceMembersPath">Property name referencing the source member to map against. Or a dot separated member path.</param>
		/// <param name="memberOptions">Member options</param>
		/// <returns>Itself</returns>
		public static IMappingExpression<TSource, TDest> Map<TSource, TDest>(this IMappingExpression<TSource, TDest> mappingExpression,
			Expression<Func<TDest, object?>>? destinationMember,
			string sourceMembersPath,
			Action<IMemberConfigurationExpression<TSource, TDest, object?>>? memberOptions = null)
		{
			if (destinationMember is null)
				return mappingExpression;

			mappingExpression.ForMember(destinationMember, o =>
			{
				o.MapFrom(sourceMembersPath);
				memberOptions?.Invoke(o);
			});
			return mappingExpression;
		}
	}
}
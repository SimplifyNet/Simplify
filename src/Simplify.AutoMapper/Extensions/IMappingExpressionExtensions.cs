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
		/// Map destination member using destination and source member expressions
		/// </summary>
		/// <typeparam name="TDest">Destination type</typeparam>
		/// <typeparam name="TSource">Source type</typeparam>
		/// <param name="mappingExpression">IMappingExpression</param>
		/// <param name="destinationMember">Destination member</param>
		/// <param name="sourceMember">Source member</param>
		/// <param name="memberOptions">Member options</param>
		/// <returns>Itself</returns>
		public static IMappingExpression<TSource, TDest> MapTo<TDest, TSource>(this IMappingExpression<TSource, TDest> mappingExpression,
			Expression<Func<TDest, object?>> destinationMember,
			Expression<Func<TSource, object?>>? sourceMember = null,
			Action<IMemberConfigurationExpression<TSource, TDest, object?>>? memberOptions = null)
		{
			if (destinationMember is null)
				throw new AutoMapperConfigurationException("Destination member expression cannot be null");

			mappingExpression.ForMember(destinationMember, o =>
			{
				o.MapFrom(sourceMember ?? (s => s));
				memberOptions?.Invoke(o);
			});
			return mappingExpression;
		}

		/// <summary>
		/// Map destination member using name and source member expressions
		/// </summary>
		/// <typeparam name="TDest">Destination type</typeparam>
		/// <typeparam name="TSource">Source type</typeparam>
		/// <param name="mappingExpression">IMappingExpression</param>
		/// <param name="destinationMember">Destination member</param>
		/// <param name="sourceMember">Source member</param>
		/// <param name="memberOptions">Member options</param>
		/// <returns>Itself</returns>
		public static IMappingExpression<TSource, TDest> MapTo<TDest, TSource>(this IMappingExpression<TSource, TDest> mappingExpression,
			string destinationMember,
			Expression<Func<TSource, object?>>? sourceMember = null,
			Action<IMemberConfigurationExpression<TSource, TDest, object?>>? memberOptions = null)
		{
			if (string.IsNullOrWhiteSpace(destinationMember))
				throw new AutoMapperConfigurationException("Destination member name cannot be null or empty");

			mappingExpression.ForMember(destinationMember, o =>
			{
				o.MapFrom(sourceMember ?? (s => s));
				memberOptions?.Invoke(o);
			});
			return mappingExpression;
		}

		/// <summary>
		/// Map destination member using expression and source member's path
		/// </summary>
		/// <typeparam name="TDest">Destination type</typeparam>
		/// <typeparam name="TSource">Source type</typeparam>
		/// <param name="mappingExpression">IMappingExpression</param>
		/// <param name="destinationMember">Destination member</param>
		/// <param name="sourceMemberPath">Property name referencing the source member to map against. Or a dot separated member path.</param>
		/// <param name="memberOptions">Member options</param>
		/// <returns>Itself</returns>
		public static IMappingExpression<TSource, TDest> MapTo<TDest, TSource>(this IMappingExpression<TSource, TDest> mappingExpression,
			Expression<Func<TDest, object?>> destinationMember,
			string sourceMemberPath,
			Action<IMemberConfigurationExpression<TSource, TDest, object?>>? memberOptions = null)
		{
			if (destinationMember is null)
				throw new AutoMapperConfigurationException("Destination member expression cannot be null");
			if (string.IsNullOrWhiteSpace(sourceMemberPath))
				throw new AutoMapperConfigurationException("Source member path cannot be null or empty");

			mappingExpression.ForMember(destinationMember, o =>
			{
				o.MapFrom(sourceMemberPath);
				memberOptions?.Invoke(o);
			});
			return mappingExpression;
		}

		/// <summary>
		/// Map destination member using name and source member's path
		/// </summary>
		/// <typeparam name="TDest">Destination type</typeparam>
		/// <typeparam name="TSource">Source type</typeparam>
		/// <param name="mappingExpression">IMappingExpression</param>
		/// <param name="destinationMember">Destination member</param>
		/// <param name="sourceMemberPath">Property name referencing the source member to map against. Or a dot separated member path.</param>
		/// <param name="memberOptions">Member options</param>
		/// <returns>Itself</returns>
		public static IMappingExpression<TSource, TDest> MapTo<TDest, TSource>(this IMappingExpression<TSource, TDest> mappingExpression,
			string destinationMember,
			string sourceMemberPath,
			Action<IMemberConfigurationExpression<TSource, TDest, object?>>? memberOptions = null)
		{
			if (string.IsNullOrWhiteSpace(destinationMember))
				throw new AutoMapperConfigurationException("Destination member name cannot be null or empty");
			if (string.IsNullOrWhiteSpace(sourceMemberPath))
				throw new AutoMapperConfigurationException("Source member path cannot be null or empty");

			mappingExpression.ForMember(destinationMember, o =>
			{
				o.MapFrom(sourceMemberPath);
				memberOptions?.Invoke(o);
			});
			return mappingExpression;
		}
	}
}
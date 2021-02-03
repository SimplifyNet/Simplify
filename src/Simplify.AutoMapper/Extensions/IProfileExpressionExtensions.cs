using AutoMapper;

namespace Simplify.AutoMapper.Extensions
{
	/// <summary>
	/// Provides extensions for the IProfileExpression
	/// </summary>
	public static class IProfileExpressionExtensions
	{
		/// <summary>
		/// Creates a mapping configuration from the Source type to the Destination type with Destination interface
		/// </summary>
		/// <typeparam name="TSource">Source type</typeparam>
		/// <typeparam name="TDestInterface">Destination interface</typeparam>
		/// <typeparam name="TDestImpl">Destination type</typeparam>
		/// <param name="profile">IProfileExpression</param>
		/// <param name="memberList">MemberList</param>
		/// <returns>Itself</returns>
		public static IMappingExpression<TSource, TDestImpl> CreateMap<TSource, TDestInterface, TDestImpl>(this IProfileExpression profile,
			MemberList? memberList = null)
			where TDestImpl : TDestInterface
		{
			if (memberList is { } m)
			{
				profile.CreateMap<TSource, TDestInterface>(m).As<TDestImpl>();
				return profile.CreateMap<TSource, TDestImpl>(m);
			}

			profile.CreateMap<TSource, TDestInterface>().As<TDestImpl>();
			return profile.CreateMap<TSource, TDestImpl>();
		}
	}
}
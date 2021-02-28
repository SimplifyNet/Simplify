using AutoMapper;

namespace Simplify.AutoMapper.Extensions
{
	/// <summary>
	/// Provides extensions for the IProfileExpression
	/// </summary>
	public static class IProfileExpressionExtensions
	{
		/// <summary>
		/// Creates a mapping configuration from the Source type to the Destination type with Destination base type
		/// </summary>
		/// <typeparam name="TSource">Source type</typeparam>
		/// <typeparam name="TDestBase">Destination base type</typeparam>
		/// <typeparam name="TDest">Destination type</typeparam>
		/// <param name="profile">IProfileExpression</param>
		/// <param name="memberList">MemberList</param>
		/// <returns>Itself</returns>
		public static IMappingExpression<TSource, TDest> CreateMap<TSource, TDestBase, TDest>(this IProfileExpression profile, MemberList? memberList = null)
			where TDest : TDestBase
		{
			if (memberList is { } m)
			{
				profile.CreateMap<TSource, TDestBase>(m).As<TDest>();
				return profile.CreateMap<TSource, TDest>(m);
			}

			profile.CreateMap<TSource, TDestBase>().As<TDest>();
			return profile.CreateMap<TSource, TDest>();
		}
	}
}
using FluentNHibernate.Mapping;

namespace Simplify.Repository.FluentNHibernate.Mappings
{
	/// <summary>
	/// Long identity object mapping
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LongIdentityObjectMap<T> : ClassMap<T>
		where T : ILongIdentityObject
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LongIdentityObjectMap{T}"/> class.
		/// </summary>
		public LongIdentityObjectMap()
		{
			Id(x => x.ID);
		}
	}
}
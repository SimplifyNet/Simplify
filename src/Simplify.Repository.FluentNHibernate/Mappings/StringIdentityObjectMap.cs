using FluentNHibernate.Mapping;

namespace Simplify.Repository.FluentNHibernate.Mappings
{
	/// <summary>
	/// String identity object mapping
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class StringIdentityObjectMap<T> : ClassMap<T>
		where T : IStringIdentityObject
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityObjectMap{T}"/> class.
		/// </summary>
		public StringIdentityObjectMap() => Id(x => x.ID);
	}
}
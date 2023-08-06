namespace Simplify.Repository.FluentNHibernate.Mappings;

/// <summary>
/// LOng named object mapping
/// </summary>
/// <typeparam name="T"></typeparam>
public class LongNamedObjectMap<T> : LongIdentityObjectMap<T>
	where T : ILongNamedObject
{
	/// <summary>
	/// Initializes a new instance of the <see cref="NamedObjectMap{T}"/> class.
	/// </summary>
	public LongNamedObjectMap() => Map(x => x.Name).Not.Nullable();
}
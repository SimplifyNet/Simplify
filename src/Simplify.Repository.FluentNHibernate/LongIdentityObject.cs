namespace Simplify.Repository.FluentNHibernate;

/// <summary>
/// Provides object with long identifier
/// </summary>
public class LongIdentityObject : ILongIdentityObject
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public virtual long ID { get; set; }
}
namespace Simplify.Repository;

/// <summary>
/// Represent object with long identifier
/// </summary>
public interface ILongIdentityObject
{
	/// <summary>
	/// Gets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	long ID { get; }
}
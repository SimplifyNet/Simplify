namespace Simplify.Repository;

/// <summary>
/// Represent object with name and identifier
/// </summary>
public interface INamedObject : IIdentityObject
{
	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>
	/// The name.
	/// </value>
	string? Name { get; set; }
}
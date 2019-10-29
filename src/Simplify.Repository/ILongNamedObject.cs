namespace Simplify.Repository
{
	/// <summary>
	/// Represent object with name and long identifier
	/// </summary>
	public interface ILongNamedObject : ILongIdentityObject
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string Name { get; set; }
	}
}
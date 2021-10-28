namespace Simplify.Repository
{
	/// <summary>
	/// Represent object with string identifier
	/// </summary>
	public interface IStringIdentityObject
	{
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		string? ID { get; set; }
	}
}
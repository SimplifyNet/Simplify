namespace Simplify.Repository.EntityFramework
{
	/// <summary>
	/// Provides object with name and long identifier
	/// </summary>
	public class LongNamedObject : LongIdentityObject, ILongNamedObject
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public virtual string? Name { get; set; }
	}
}
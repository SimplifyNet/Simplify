namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides object with name and identifier
	/// </summary>
	public class NamedObject : IdentityObject, INamedObject
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
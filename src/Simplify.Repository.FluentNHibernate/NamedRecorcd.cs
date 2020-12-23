namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides record object with name and identifier
	/// </summary>
	public record NamedRecord : IdentityRecord, INamedObject
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public virtual string Name { get; set; }
	}
}
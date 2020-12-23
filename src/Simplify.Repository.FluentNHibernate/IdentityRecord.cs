namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides record object with identifier
	/// </summary>
	public record IdentityRecord : IIdentityObject
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public virtual int ID { get; set; }
	}
}
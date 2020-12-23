namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides record object with long identifier
	/// </summary>
	public record LongIdentityRecord : ILongIdentityObject
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public virtual long ID { get; set; }
	}
}
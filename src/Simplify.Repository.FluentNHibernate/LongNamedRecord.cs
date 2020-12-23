namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides record object with name and long identifier
	/// </summary>
	public record LongNamedRecord : LongIdentityRecord, ILongNamedObject
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
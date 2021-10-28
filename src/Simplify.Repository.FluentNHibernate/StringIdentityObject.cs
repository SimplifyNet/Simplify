namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides object with string identifier
	/// </summary>
	public class StringIdentityObject : IStringIdentityObject
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public virtual string? ID { get; set; }
	}
}
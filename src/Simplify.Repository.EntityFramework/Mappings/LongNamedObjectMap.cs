using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simplify.Repository.EntityFramework.Mappings
{
	/// <summary>
	/// Long named object mapping
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LongNamedObjectMap<T> : LongIdentityObjectMap<T>
		where T : class, ILongNamedObject
	{
		/// <summary>
		/// Adds entity additional configurations.
		/// </summary>
		/// <param name="builder">The builder.</param>
		protected override void ConfigureEntity(EntityTypeBuilder<T> builder) => builder.Property(x => x.Name).IsRequired();
	}
}
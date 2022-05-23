using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simplify.Repository.EntityFramework.Mappings;

/// <summary>
/// Named object mapping
/// </summary>
/// <typeparam name="T"></typeparam>
public class NamedObjectMap<T> : IdentityObjectMap<T>
	where T : class, INamedObject
{
	/// <summary>
	/// Adds entity additional configurations.
	/// </summary>
	/// <param name="builder">The builder.</param>
	protected override void ConfigureEntity(EntityTypeBuilder<T> builder) => builder.Property(x => x.Name).IsRequired();
}
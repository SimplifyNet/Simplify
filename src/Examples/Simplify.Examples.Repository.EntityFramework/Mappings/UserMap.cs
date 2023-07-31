using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simplify.Examples.Repository.EntityFramework.Accounts;
using Simplify.Repository.EntityFramework.Mappings;

namespace Simplify.Examples.Repository.EntityFramework.Mappings;

public class UserMap : NamedObjectMap<User>
{
	protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
	{
		builder.HasOne(x => x.City)
			.WithMany();

		builder.Property(x => x.Password);
		builder.Property(x => x.EMail);
		builder.Property(x => x.LastActivityTime);

		base.ConfigureEntity(builder);
	}
}
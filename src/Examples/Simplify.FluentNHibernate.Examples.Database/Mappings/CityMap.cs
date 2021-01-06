﻿using Simplify.FluentNHibernate.Examples.Database.Location;
using Simplify.Repository.FluentNHibernate.Mappings;

namespace Simplify.FluentNHibernate.Examples.Database.Mappings
{
	public class CityMap : IdentityObjectMap<City>
	{
		public CityMap()
		{
			Table("Cities");

			HasMany<CityName>(x => x.CityNames)
				.Inverse()
				.Cascade.All()
				.Cascade.AllDeleteOrphan();
		}
	}
}
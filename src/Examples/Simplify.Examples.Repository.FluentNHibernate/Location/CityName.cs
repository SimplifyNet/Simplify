using Simplify.Examples.Repository.Domain.Location;
using Simplify.Repository.FluentNHibernate;

namespace Simplify.Examples.Repository.FluentNHibernate.Location;

public class CityName : NamedObject, ICityName
{
	public virtual ICity City { get; set; }

	public virtual string Language { get; set; }
}
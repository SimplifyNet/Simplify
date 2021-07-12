using Simplify.Examples.Repository.Domain.Location;
using Simplify.Repository.EntityFramework;

namespace Simplify.Examples.Repository.EntityFramework.Location
{
	public class CityName : NamedObject, ICityName
	{
		public ICity City { get; set; }

		public string Language { get; set; }
	}
}
using System;
using System.Linq;
using Simplify.Repository;

namespace Simplify.Examples.Repository.Domain.Location;

public class CitiesService(IGenericRepository<ICity> repository) : ICitiesService
{
	public ICity GetCity(string cityName)
	{
		ArgumentNullException.ThrowIfNull(cityName);

		return repository.GetSingleByQuery(x => x.CityNames.Any(n => n.Name == cityName));
	}
}
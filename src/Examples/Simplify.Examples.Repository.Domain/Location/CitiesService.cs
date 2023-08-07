﻿using System;
using System.Linq;
using Simplify.Repository;

namespace Simplify.Examples.Repository.Domain.Location;

public class CitiesService : ICitiesService
{
	private readonly IGenericRepository<ICity> _repository;

	public CitiesService(IGenericRepository<ICity> repository)
	{
		_repository = repository;
	}

	public ICity GetCity(string cityName)
	{
		if (cityName == null) throw new ArgumentNullException(nameof(cityName));

		return _repository.GetSingleByQuery(x => x.CityNames.Any(n => n.Name == cityName));
	}
}
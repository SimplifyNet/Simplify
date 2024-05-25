using System.Data;

namespace Simplify.Examples.Repository.Domain.Location;

public class TransactCitiesService(ICitiesService baseService, IExampleUnitOfWork unitOfWork) : ICitiesService
{
	public ICity GetCity(string cityName)
	{
		unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted);

		var item = baseService.GetCity(cityName);

		unitOfWork.Commit();

		return item;
	}
}
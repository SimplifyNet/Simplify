using System.Data;
using Simplify.FluentNHibernate.Examples.Domain.Location;

namespace Simplify.FluentNHibernate.Examples.Domain.Accounts
{
	public class TransactUsersService : IUsersService
	{
		private readonly IUsersService _baseService;
		private readonly IExampleUnitOfWork _unitOfWork;

		public TransactUsersService(IUsersService baseService, IExampleUnitOfWork unitOfWork)
		{
			_baseService = baseService;
			_unitOfWork = unitOfWork;
		}

		public IUser GetUser(string userName)
		{
			_unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted);

			var item = _baseService.GetUser(userName);

			_unitOfWork.Commit();

			return item;
		}

		public void SetUserCity(IUser user, ICity city)
		{
			_unitOfWork.BeginTransaction();

			_baseService.SetUserCity(user, city);

			_unitOfWork.Commit();
		}
	}
}
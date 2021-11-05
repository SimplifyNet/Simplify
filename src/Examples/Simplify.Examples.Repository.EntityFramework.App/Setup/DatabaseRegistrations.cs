using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.EntityFramework;
using Simplify.Examples.Repository.Domain;
using Simplify.Examples.Repository.Domain.Accounts;
using Simplify.Examples.Repository.Domain.Location;
using Simplify.Repository;
using Simplify.Repository.EntityFramework;

namespace Simplify.Examples.Repository.EntityFramework.App.Setup
{
	public static class DatabaseRegistrations
	{
		public static IDIRegistrator RegisterDatabase(this IDIRegistrator registrator) =>
			registrator.Register(r => new DbContextOptionsBuilder()
					.UseSqlServer(SettingsBasedConnectionString.Build(r.Resolve<IConfiguration>(), "ExampleDatabaseConnectionSettings")).Options, LifetimeType.Singleton)
				.Register<ExampleDbContext>()
				.Register<ExampleUnitOfWork>()
				.Register<IExampleUnitOfWork>(r => r.Resolve<ExampleUnitOfWork>())

				.Register<IGenericRepository<IUser>>(r => new GenericRepository<IUser>(r.Resolve<ExampleUnitOfWork>().Context))
				.Register<UsersService>()
				.Register<IUsersService>(r => new TransactUsersService(r.Resolve<UsersService>(), r.Resolve<IExampleUnitOfWork>()))

				.Register<IGenericRepository<ICity>>(r => new GenericRepository<ICity>(r.Resolve<ExampleUnitOfWork>().Context))
				.Register<CitiesService>()
				.Register<ICitiesService>(r => new TransactCitiesService(r.Resolve<CitiesService>(), r.Resolve<IExampleUnitOfWork>()));
	}
}
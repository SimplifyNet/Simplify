using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.FluentNHibernate.Examples.Database;
using Simplify.FluentNHibernate.Examples.Domain;
using Simplify.FluentNHibernate.Examples.Domain.Accounts;
using Simplify.FluentNHibernate.Examples.Domain.Location;
using Simplify.Repository;
using Simplify.Repository.FluentNHibernate;

namespace Simplify.FluentNHibernate.Examples.App.Setup
{
	public static class DatabaseRegistrations
	{
		public static IDIRegistrator RegisterDatabase(this IDIRegistrator registrator)
		{
			registrator.Register(r => (ExampleSessionFactoryBuilder)new ExampleSessionFactoryBuilder(r.Resolve<IConfiguration>()).Build(), LifetimeType.Singleton)
				.Register(r => new ExampleUnitOfWork(r.Resolve<ExampleSessionFactoryBuilder>().Instance))
				.Register<IExampleUnitOfWork>(r => r.Resolve<ExampleUnitOfWork>());

			registrator.Register<IGenericRepository<IUser>>(r => new GenericRepository<IUser>(r.Resolve<ExampleUnitOfWork>().Session))
				.Register<UsersService>()
				.Register<IUsersService>(r => new TransactUsersService(r.Resolve<UsersService>(), r.Resolve<IExampleUnitOfWork>()));

			registrator.Register<IGenericRepository<ICity>>(r => new GenericRepository<ICity>(r.Resolve<ExampleUnitOfWork>().Session))
				.Register<CitiesService>()
				.Register<ICitiesService>(r => new TransactCitiesService(r.Resolve<CitiesService>(), r.Resolve<IExampleUnitOfWork>()));

			return registrator;
		}
	}
}
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Simplify.FluentNHibernate;
using Simplify.FluentNHibernate.Conventions;
using Simplify.Repository.FluentNHibernate.Tests.Entities.Accounts;
using Simplify.Repository.FluentNHibernate.Tests.Mappings.Accounts;

namespace Simplify.Repository.FluentNHibernate.Tests
{
	public class TransactUnitOfWorkTests
	{
		private ISessionFactory _sessionFactory;

		[OneTimeSetUp]
		public void Initialize()
		{
			CreateDatabaseAndSessionFactory(CreateConfiguration());
		}

		[Test]
		public async Task UnitOfWorkLifetimeTest()
		{
			// Arrange
			var uow = new TestUnitOfWork(_sessionFactory);

			// Act

			// 1

			uow.BeginTransaction();
			await uow.Session.GetAsync<User>(1);
			await uow.CommitAsync();

			// 2

			uow.BeginTransaction();
			await uow.Session.GetAsync<User>(1);
			await uow.CommitAsync();

			// 3

			uow.BeginTransaction();
			await uow.Session.GetAsync<User>(1);

			// Cleanup
			uow.Dispose();
		}

		private static FluentConfiguration CreateConfiguration()
		{
			return Fluently.Configure()
				.InitializeFromConfigSqLite("Test.sqlite", true)
				.AddMappingsFromAssemblyOf<UserMap>(PrimaryKey.Name.Is(x => "ID"), ForeignKeyConstraintNameConvention.WithConstraintNameConvention());
		}

		private void CreateDatabaseAndSessionFactory(FluentConfiguration configuration)
		{
			Configuration config = null;
			configuration.ExposeConfiguration(c => config = c);
			_sessionFactory = configuration.BuildSessionFactory();

			config.CreateIndexesForForeignKeys();

			using var session = _sessionFactory.OpenSession();

			var export = new SchemaExport(config);
			export.Execute(false, true, false, session.Connection, null);
		}
	}
}
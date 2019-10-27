using System;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Simplify.FluentNHibernate.Tests.Entities.Accounts;
using Simplify.FluentNHibernate.Tests.Mappings.Accounts;

namespace Simplify.FluentNHibernate.Tests
{
	public class SessionExtensionsTestsBase
	{
		protected readonly Expression<Func<User, bool>> SingleObjectQuery = x => x.Name == "test";

		public void CreateDatabase(Func<ISessionFactory, DbConnection> sessionBuilder, bool inMemory = true)
		{
			if (sessionBuilder == null) throw new ArgumentNullException(nameof(sessionBuilder));

			var configuration = inMemory ? CreateConfigurationInMemory() : CreateConfiguration();

			Configuration config = null;
			configuration.ExposeConfiguration(c => config = c);
			var factory = configuration.BuildSessionFactory();

			var export = new SchemaExport(config);
			export.Execute(false, true, false, sessionBuilder(factory), null);
		}

		protected static void PerformSingleObjectNotExistTest(Func<User> act)
		{
			// Act
			var result = act();

			// Assert
			Assert.IsNull(result);
		}

		protected static async Task PerformSingleObjectNotExistAsyncTest(Func<Task<User>> act)
		{
			// Act
			var result = await act();

			// Assert
			Assert.IsNull(result);
		}

		protected void PerformSingleObjectExistTest(Func<User> act, Action userCreator)
		{
			// Arrange

			userCreator();

			// Act
			var result = act();

			// Assert
			Assert.IsNotNull(result);
		}

		protected async Task PerformSingleObjectExistAsyncTest(Func<Task<User>> act, Action userCreator)
		{
			// Arrange

			userCreator();

			// Act
			var result = await act();

			// Assert
			Assert.IsNotNull(result);
		}

		private static FluentConfiguration CreateConfigurationInMemory()
		{
			return Fluently.Configure()
				.InitializeFromConfigSqLiteInMemory(true)
				.AddMappingsFromAssemblyOf<UserMap>(PrimaryKey.Name.Is(x => "ID"));
		}

		private static FluentConfiguration CreateConfiguration()
		{
			return Fluently.Configure()
				.InitializeFromConfigSqLite("Test.sqlite", true)
				.AddMappingsFromAssemblyOf<UserMap>(PrimaryKey.Name.Is(x => "ID"));
		}
	}
}
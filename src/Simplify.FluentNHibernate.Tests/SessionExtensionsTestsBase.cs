using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
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

namespace Simplify.FluentNHibernate.Tests;

public class SessionExtensionsTestsBase
{
	protected readonly Expression<Func<User, bool>> SingleObjectQuery = x => x.Name == "test";
	protected readonly Expression<Func<User, bool>> MultipleObjectsQuery = x => x.LastActivityTime == new DateTime(2015, 2, 3, 14, 16, 0, DateTimeKind.Local);

	protected readonly int PagedPageIndex = 1;
	protected readonly int PagedItemsPerPage = 2;
	protected readonly Expression<Func<User, bool>> PagedQuery = x => x.Name.Contains("test");
	protected readonly Func<IQueryable<User>, IQueryable<User>> PagedCustomProcessing = x => x.OrderByDescending(o => o.LastActivityTime);

	public void CreateDatabase(Func<ISessionFactory, DbConnection> sessionBuilder, bool inMemory = true)
	{
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
		Assert.That(result, Is.Null);
	}

	protected static async Task PerformSingleObjectNotExistAsyncTest(Func<Task<User>> act)
	{
		// Act
		var result = await act();

		// Assert
		Assert.That(result, Is.Null);
	}

	protected static void PerformSingleObjectMultipleExistTest(Func<User> act, Action userCreator)
	{
		// Arrange

		userCreator();

		// Act & Assert
		Assert.That(() => act(), Throws.InvalidOperationException);
	}

	protected static void PerformMultipleObjectPagedRetrieveTest(Func<IList<User>> act)
	{
		// Act
		var items = act();

		// Assert

		Assert.That(items.Count, Is.EqualTo(2));
		Assert.That(items[0].Name, Is.EqualTo("test5"));
		Assert.That(items[1].Name, Is.EqualTo("test0"));
	}

	protected static async Task PerformSingleObjectExistAsyncTest(Func<Task<User>> act, Action userCreator)
	{
		// Arrange

		userCreator();

		// Act
		var result = await act();

		// Assert
		Assert.That(result, Is.Not.Null);
	}

	protected static void PerformSingleObjectMultipleExistAsyncTest(Func<Task<User>> act, Action userCreator)
	{
		// Arrange

		userCreator();

		// Act
		Assert.That(async () => await act(), Throws.InvalidOperationException);
	}

	protected static void PerformMultipleObjectsRetrieveTest(Func<IList<User>> act)
	{
		// Act
		var items = act();

		// Assert

		Assert.That(items.Count, Is.EqualTo(2));
		Assert.That(items[0].Name, Is.EqualTo("test5"));
		Assert.That(items[1].Name, Is.EqualTo("foo1"));
	}

	protected static void PerformSingleObjectExistTest(Func<User> act, Action userCreator)
	{
		// Arrange

		userCreator();

		// Act
		var result = act();

		// Assert
		Assert.That(result, Is.Not.Null);
	}

	protected static async Task PerformMultipleObjectsRetrieveAsyncTest(Func<Task<IList<User>>> act)
	{
		// Act
		var items = await act();

		// Assert

		Assert.That(items.Count, Is.EqualTo(2));
		Assert.That(items[0].Name, Is.EqualTo("test5"));
		Assert.That(items[1].Name, Is.EqualTo("foo1"));
	}

	protected static async Task PerformMultipleObjectPagedRetrieveAsyncTest(Func<Task<IList<User>>> act)
	{
		// Act
		var items = await act();

		// Assert

		Assert.That(items.Count, Is.EqualTo(2));
		Assert.That(items[0].Name, Is.EqualTo("test5"));
		Assert.That(items[1].Name, Is.EqualTo("test0"));
	}

	protected static void PerformCountTest(Func<int> act)
	{
		// Act
		var result = act();

		// Assert

		Assert.That(result, Is.EqualTo(5));
	}

	protected static async Task PerformCountAsyncTest(Func<Task<int>> act)
	{
		// Act
		var result = await act();

		// Assert

		Assert.That(result, Is.EqualTo(5));
	}

	protected static void PerformLongCountTest(Func<long> act)
	{
		// Act
		var result = act();

		// Assert

		Assert.That(result, Is.EqualTo(5));
	}

	protected static async Task PerformLongCountAsyncTest(Func<Task<long>> act)
	{
		// Act
		var result = await act();

		// Assert

		Assert.That(result, Is.EqualTo(5));
	}

	private static FluentConfiguration CreateConfigurationInMemory()
	{
		return Fluently.Configure()
			.InitializeFromConfigSqLiteInMemory(true)
			.AddMappingsFromAssemblyOf<UserMap>(PrimaryKey.Name.Is(_ => "ID"));
	}

	private static FluentConfiguration CreateConfiguration()
	{
		return Fluently.Configure()
			.InitializeFromConfigSqLite("Test.sqlite", true)
			.AddMappingsFromAssemblyOf<UserMap>(PrimaryKey.Name.Is(_ => "ID"));
	}
}
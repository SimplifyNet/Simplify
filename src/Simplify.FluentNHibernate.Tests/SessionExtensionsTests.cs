using System;
using System.Linq;
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
	[TestFixture]
	public class SessionExtensionsTests
	{
		private ISession _session;

		[SetUp]
		public void Initialize()
		{
			var configuration =
			Fluently.Configure()
			.InitializeFromConfigSqLiteInMemory(true)
			.AddMappingsFromAssemblyOf<UserMap>(PrimaryKey.Name.Is(x => "ID"));

			Configuration config = null;
			configuration.ExposeConfiguration(c => config = c);
			var factory = configuration.BuildSessionFactory();
			_session = factory.OpenSession();

			var export = new SchemaExport(config);
			export.Execute(false, true, false, _session.Connection, null);
		}

		[Test]
		public void GetObject_DfferentCountions_Met()
		{
			// Act & Assert
			Assert.IsNull(_session.GetObject<User>(x => x.Name == "test"));

			// Assign

			_session.Save(new User { Name = "test" });
			_session.Flush();

			// Act
			var user = _session.GetObject<User>(x => x.Name == "test");

			// Assert
			Assert.IsNotNull(user);

			// Assign

			user.Name = "foo";
			_session.Update(user);
			_session.Flush();

			// Act
			user = _session.GetObject<User>(x => x.Name == "foo");

			// Assert
			Assert.IsNotNull(user);

			// Assign

			_session.Delete(user);
			_session.Flush();

			// Act & Assert
			Assert.IsNull(_session.GetObject<User>(x => x.Name == "foo"));
		}

		[Test]
		public void GetListPagedAndGetCount_MultipleItems_CorrectCountAndPage()
		{
			// Assign

			_session.Save(new User { Name = "test0", LastActivityTime = new DateTime(2015, 2, 3, 14, 15, 0) });
			_session.Save(new User { Name = "test1", LastActivityTime = new DateTime(2015, 2, 3, 14, 19, 0) });
			_session.Save(new User { Name = "foo2", LastActivityTime = new DateTime(2015, 2, 3, 14, 17, 0) });
			_session.Save(new User { Name = "test3", LastActivityTime = new DateTime(2015, 2, 3, 14, 18, 0) });
			_session.Save(new User { Name = "test4", LastActivityTime = new DateTime(2015, 2, 3, 14, 14, 0) });
			_session.Save(new User { Name = "test5", LastActivityTime = new DateTime(2015, 2, 3, 14, 16, 0) });
			_session.Save(new User { Name = "foo1", LastActivityTime = new DateTime(2015, 2, 3, 14, 16, 0) });

			_session.Flush();

			// Act
			var items = _session.GetListPaged<User>(1, 2, x => x.Name.Contains("test"), x => x.OrderByDescending(o => o.LastActivityTime));
			var itemsCount = _session.GetCount<User>(x => x.Name.Contains("test"));

			// Assert

			Assert.AreEqual(2, items.Count);
			Assert.AreEqual(5, itemsCount);
			Assert.AreEqual("test5", items[0].Name);
			Assert.AreEqual("test0", items[1].Name);
		}
	}
}
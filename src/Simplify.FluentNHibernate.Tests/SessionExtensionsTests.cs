using NHibernate;

using NUnit.Framework;
using Simplify.FluentNHibernate.Tests.Entities.Accounts;

namespace Simplify.FluentNHibernate.Tests
{
	[TestFixture]
	public class SessionExtensionsTests : SessionExtensionsTestsBase
	{
		private ISession _session;

		[SetUp]
		public void Initialize()
		{
			CreateDatabase(x => (_session = x.OpenSession()).Connection);
		}

		[Test]
		public void GetSingleObject_NotExist_ReturnNull()
		{
			// Act
			var result = _session.GetSingleObject<User>(x => x.Name == "test");

			// Assert
			Assert.IsNull(result);
		}

		[Test]
		public void GetSingleObject_Exist_ReturnNotNull()
		{
			// Arrange

			_session.Save(new User { Name = "test" });
			_session.Flush();

			// Act
			var result = _session.GetSingleObject<User>(x => x.Name == "test");

			// Assert
			Assert.IsNotNull(result);
		}

		// TODO refactor to separate tests with good code coverage

		//[Test]
		//public void GetObject_DfferentCountions_Met()
		//{
		//// Assign

		//// Act
		//var user = _session.GetObject<User>(x => x.Name == "test");

		//// Assert

		//// Assign

		//user.Name = "foo";
		//_session.Update(user);
		//_session.Flush();

		//// Act
		//user = _session.GetObject<User>(x => x.Name == "foo");

		//// Assert
		//Assert.IsNotNull(user);

		//// Assign

		//_session.Delete(user);
		//_session.Flush();

		//// Act & Assert
		//Assert.IsNull(_session.GetObject<User>(x => x.Name == "foo"));
		//}

		//[Test]
		//public void GetListPagedAndGetCount_MultipleItems_CorrectCountAndPage()
		//{
		//	// Assign

		//	_session.Save(new User { Name = "test0", LastActivityTime = new DateTime(2015, 2, 3, 14, 15, 0) });
		//	_session.Save(new User { Name = "test1", LastActivityTime = new DateTime(2015, 2, 3, 14, 19, 0) });
		//	_session.Save(new User { Name = "foo2", LastActivityTime = new DateTime(2015, 2, 3, 14, 17, 0) });
		//	_session.Save(new User { Name = "test3", LastActivityTime = new DateTime(2015, 2, 3, 14, 18, 0) });
		//	_session.Save(new User { Name = "test4", LastActivityTime = new DateTime(2015, 2, 3, 14, 14, 0) });
		//	_session.Save(new User { Name = "test5", LastActivityTime = new DateTime(2015, 2, 3, 14, 16, 0) });
		//	_session.Save(new User { Name = "foo1", LastActivityTime = new DateTime(2015, 2, 3, 14, 16, 0) });

		//	_session.Flush();

		//	// Act
		//	var items = _session.GetListPaged<User>(1, 2, x => x.Name.Contains("test"), x => x.OrderByDescending(o => o.LastActivityTime));
		//	var itemsCount = _session.GetCount<User>(x => x.Name.Contains("test"));

		//	// Assert

		//	Assert.AreEqual(2, items.Count);
		//	Assert.AreEqual(5, itemsCount);
		//	Assert.AreEqual("test5", items[0].Name);
		//	Assert.AreEqual("test0", items[1].Name);
		//}
	}
}
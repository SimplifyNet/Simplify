using NHibernate;
using NUnit.Framework;

namespace Simplify.FluentNHibernate.Tests
{
	[TestFixture]
	public class SessionExtensionsMultipleObjectsTests : SessionExtensionsTestsBase
	{
		private ISession _session;

		[SetUp]
		public void Initialize()
		{
			CreateDatabase(x => (_session = x.OpenSession()).Connection);
		}

		// TODO refactor to separate tests with good code coverage

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
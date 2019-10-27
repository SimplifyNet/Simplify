﻿using NHibernate;
using NUnit.Framework;

namespace Simplify.FluentNHibernate.Tests
{
	[TestFixture]
	public class StatelessSessionExtensionsTests : SessionExtensionsTestsBase
	{
		private IStatelessSession _session;

		[SetUp]
		public void Initialize()
		{
			CreateDatabase(x => (_session = x.OpenStatelessSession()).Connection);
		}

		// TODO refactor same as SessionExtensionsTests

		//[Test]
		//public void Stateless_GetObject_Tests()
		//{
		//	Assert.IsNull(_session.GetObject<User>(x => x.Name == "test"));

		//	_session.Insert(new User { Name = "test" });

		//	var user = _session.GetObject<User>(x => x.Name == "test");
		//	Assert.IsNotNull(user);

		//	user.Name = "foo";
		//	_session.Update(user);

		//	user = _session.GetObject<User>(x => x.Name == "foo");
		//	Assert.IsNotNull(user);

		//	_session.Delete(user);
		//	Assert.IsNull(_session.GetObject<User>(x => x.Name == "foo"));
		//}

		//[Test]
		//public void Stateless_GetListPaged_Tests()
		//{
		//	// Act

		//	_session.Insert(new User { Name = "test0", LastActivityTime = new DateTime(2015, 2, 3, 14, 15, 0) });
		//	_session.Insert(new User { Name = "test1", LastActivityTime = new DateTime(2015, 2, 3, 14, 19, 0) });
		//	_session.Insert(new User { Name = "foo2", LastActivityTime = new DateTime(2015, 2, 3, 14, 17, 0) });
		//	_session.Insert(new User { Name = "test3", LastActivityTime = new DateTime(2015, 2, 3, 14, 18, 0) });
		//	_session.Insert(new User { Name = "test4", LastActivityTime = new DateTime(2015, 2, 3, 14, 14, 0) });
		//	_session.Insert(new User { Name = "test5", LastActivityTime = new DateTime(2015, 2, 3, 14, 16, 0) });
		//	_session.Insert(new User { Name = "foo1", LastActivityTime = new DateTime(2015, 2, 3, 14, 16, 0) });

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
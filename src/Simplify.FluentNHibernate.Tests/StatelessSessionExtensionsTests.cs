using System.Threading.Tasks;
using NHibernate;
using NUnit.Framework;
using Simplify.FluentNHibernate.Tests.Entities.Accounts;

namespace Simplify.FluentNHibernate.Tests
{
	[TestFixture]
	public class StatelessSessionExtensionsTests : SessionExtensionsTestsBase
	{
		private IStatelessSession _session;

		[SetUp]
		public void Initialize()
		{
			CreateDatabase(x => (_session = x.OpenStatelessSession()).Connection, false);
		}

		[Test]
		public void GetSingleObject_NotExist_ReturnNull()
		{
			PerformSingleObjectNotExistTest(() => _session.GetSingleObject(SingleObjectQuery));
		}

		[Test]
		public async Task GetSingleObjectAsync_NotExist_ReturnNull()
		{
			await PerformSingleObjectNotExistAsyncTest(() => _session.GetSingleObjectAsync(SingleObjectQuery));
		}

		[Test]
		public void GetSingleObject_Exist_ReturnNotNull()
		{
			PerformSingleObjectExistTest(() => _session.GetSingleObject(SingleObjectQuery), CreateTestUser);
		}

		[Test]
		public async Task GetSingleObjectAsync_Exist_ReturnNotNull()
		{
			await PerformSingleObjectExistAsyncTest(() => _session.GetSingleObjectAsync(SingleObjectQuery), CreateTestUser);
		}

		[Test]
		public void GetSingleObjectCacheable_NotExist_ReturnNull()
		{
			PerformSingleObjectNotExistTest(() => _session.GetSingleObjectCacheable(SingleObjectQuery));
		}

		[Test]
		public async Task GetSingleObjectCacheableAsync_NotExist_ReturnNull()
		{
			await PerformSingleObjectNotExistAsyncTest(() => _session.GetSingleObjectCacheableAsync(SingleObjectQuery));
		}

		[Test]
		public void GetSingleObjectCacheable_Exist_ReturnNull()
		{
			PerformSingleObjectExistTest(() => _session.GetSingleObjectCacheable(SingleObjectQuery), CreateTestUser);
		}

		[Test]
		public async Task GetSingleObjectCacheableAsync_Exist_ReturnNull()
		{
			await PerformSingleObjectExistAsyncTest(() => _session.GetSingleObjectCacheableAsync(SingleObjectQuery), CreateTestUser);
		}

		private void CreateTestUser()
		{
			_session.Insert(new User { Name = "test" });
		}

		// TODO refactor same as SessionExtensionsTests

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
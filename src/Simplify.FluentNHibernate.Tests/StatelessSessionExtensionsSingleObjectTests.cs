using System.Threading.Tasks;
using NHibernate;
using NUnit.Framework;
using Simplify.FluentNHibernate.Tests.Entities.Accounts;

namespace Simplify.FluentNHibernate.Tests
{
	[TestFixture]
	public class StatelessSessionExtensionsSingleObjectTests : SessionExtensionsTestsBase
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
		public void GetSingleObject_ExistMultiple_InvalidOperationException()
		{
			PerformSingleObjectMultipleExistTest(() => _session.GetSingleObject(SingleObjectQuery), CreateMultipleTestUsers);
		}

		[Test]
		public void GetSingleObjectAsync_ExistMultiple_InvalidOperationException()
		{
			PerformSingleObjectMultipleExistAsyncTest(() => _session.GetSingleObjectAsync(SingleObjectQuery), CreateMultipleTestUsers);
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

		[Test]
		public void GetSingleObjectCacheable_ExistMultiple_InvalidOperationException()
		{
			PerformSingleObjectMultipleExistTest(() => _session.GetSingleObjectCacheable(SingleObjectQuery), CreateMultipleTestUsers);
		}

		[Test]
		public void GetSingleObjectCacheableAsync_ExistMultiple_InvalidOperationException()
		{
			PerformSingleObjectMultipleExistAsyncTest(() => _session.GetSingleObjectCacheableAsync(SingleObjectQuery), CreateMultipleTestUsers);
		}

		[Test]
		public void GetFirstObject_Exist_ReturnNull()
		{
			PerformSingleObjectExistTest(() => _session.GetFirstObject(SingleObjectQuery), CreateTestUser);
		}

		[Test]
		public async Task GetFirstObjectAsync_Exist_ReturnNull()
		{
			await PerformSingleObjectExistAsyncTest(() => _session.GetFirstObjectAsync(SingleObjectQuery), CreateTestUser);
		}

		[Test]
		public void GetFirstObject_MultipleExist_FirstReturned()
		{
			// Arrange
			CreateMultipleTestUsers();

			// Act
			var result = _session.GetFirstObject(SingleObjectQuery);

			// Assert
			Assert.AreEqual(1, result.ID);
		}

		[Test]
		public async Task GetFirstObjectAsync_MultipleExist_FirstReturned()
		{
			// Arrange
			CreateMultipleTestUsers();

			// Act
			var result = await _session.GetFirstObjectAsync(SingleObjectQuery);

			// Assert
			Assert.AreEqual(1, result.ID);
		}

		private void CreateTestUser()
		{
			_session.Insert(new User { Name = "test" });
		}

		private void CreateMultipleTestUsers()
		{
			_session.Insert(new User { Name = "test", ID = 1 });
			_session.Insert(new User { Name = "test", ID = 2 });
		}
	}
}
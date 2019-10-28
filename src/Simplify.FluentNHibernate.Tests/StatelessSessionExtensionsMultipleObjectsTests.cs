using System;
using System.Threading.Tasks;
using NHibernate;
using NUnit.Framework;
using Simplify.FluentNHibernate.Tests.Entities.Accounts;

namespace Simplify.FluentNHibernate.Tests
{
	[TestFixture]
	public class StatelessSessionExtensionsMultipleObjectsTests : SessionExtensionsTestsBase
	{
		private IStatelessSession _session;

		[SetUp]
		public void Initialize()
		{
			CreateDatabase(x => (_session = x.OpenStatelessSession()).Connection, false);
			CreateUsers();
		}

		[Test]
		public void GetList_MultipleExist_CorrectObjectsReturned()
		{
			PerformMultipleObjectsRetrieveTest(() => _session.GetList(MultipleObjectsQuery));
		}

		[Test]
		public async Task GetListAsync_MultipleExist_CorrectObjectsReturned()
		{
			await PerformMultipleObjectsRetrieveAsyncTest(() => _session.GetListAsync(MultipleObjectsQuery));
		}

		[Test]
		public void GetListPaged_MultipleExist_CorrectObjectsReturned()
		{
			PerformMultipleObjectPagedRetrieveTest(() => _session.GetListPaged(PagedPageIndex, PagedItemsPerPage, PagedQuery, PagedCustomProcessing));
		}

		[Test]
		public async Task GetListPagedAsync_MultipleExist_CorrectObjectsReturned()
		{
			await PerformMultipleObjectPagedRetrieveAsyncTest(() => _session.GetListPagedAsync(PagedPageIndex, PagedItemsPerPage, PagedQuery, PagedCustomProcessing));
		}

		[Test]
		public void GetCount_MultipleExist_CorrectCountReturned()
		{
			PerformCountTest(() => _session.GetCount(PagedQuery));
		}

		[Test]
		public async Task GetCountAsync_MultipleExist_CorrectCountReturned()
		{
			await PerformCountAsyncTest((() => _session.GetCountAsync(PagedQuery)));
		}

		[Test]
		public void GetLongCount_MultipleExist_CorrectCountReturned()
		{
			PerformLongCountTest(() => _session.GetLongCount(PagedQuery));
		}

		[Test]
		public async Task GetLongCountAsync_MultipleExist_CorrectCountReturned()
		{
			await PerformLongCountAsyncTest((() => _session.GetLongCountAsync(PagedQuery)));
		}

		private void CreateUsers()
		{
			_session.Insert(new User { Name = "test0", LastActivityTime = new DateTime(2015, 2, 3, 14, 15, 0) });
			_session.Insert(new User { Name = "test1", LastActivityTime = new DateTime(2015, 2, 3, 14, 19, 0) });
			_session.Insert(new User { Name = "foo2", LastActivityTime = new DateTime(2015, 2, 3, 14, 17, 0) });
			_session.Insert(new User { Name = "test3", LastActivityTime = new DateTime(2015, 2, 3, 14, 18, 0) });
			_session.Insert(new User { Name = "test4", LastActivityTime = new DateTime(2015, 2, 3, 14, 14, 0) });
			_session.Insert(new User { Name = "test5", LastActivityTime = new DateTime(2015, 2, 3, 14, 16, 0) });
			_session.Insert(new User { Name = "foo1", LastActivityTime = new DateTime(2015, 2, 3, 14, 16, 0) });
		}
	}
}
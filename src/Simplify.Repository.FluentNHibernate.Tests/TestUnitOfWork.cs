using NHibernate;

namespace Simplify.Repository.FluentNHibernate.Tests
{
	public class TestUnitOfWork : TransactUnitOfWork
	{
		public TestUnitOfWork(ISessionFactory sessionFactory) : base(sessionFactory)
		{
		}
	}
}
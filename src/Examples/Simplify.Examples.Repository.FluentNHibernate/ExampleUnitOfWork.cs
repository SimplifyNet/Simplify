using NHibernate;
using Simplify.Examples.Repository.Domain;
using Simplify.Repository.FluentNHibernate;

namespace Simplify.Examples.Repository.FluentNHibernate
{
	public class ExampleUnitOfWork : TransactUnitOfWork, IExampleUnitOfWork
	{
		public ExampleUnitOfWork(ISessionFactory sessionFactory) : base(sessionFactory)
		{
		}
	}
}
using Simplify.Examples.Repository.Domain;
using Simplify.Repository.EntityFramework;

namespace Simplify.Examples.Repository.EntityFramework
{
	public class ExampleUnitOfWork : TransactUnitOfWork<ExampleDbContext>, IExampleUnitOfWork
	{
		public ExampleUnitOfWork(ExampleDbContext context) : base(context)
		{
		}
	}
}
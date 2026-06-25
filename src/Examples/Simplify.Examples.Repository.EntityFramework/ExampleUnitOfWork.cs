using Simplify.Examples.Repository.Domain;
using Simplify.Repository.EntityFramework;

namespace Simplify.Examples.Repository.EntityFramework;

public class ExampleUnitOfWork(ExampleDbContext context) : TransactUnitOfWork<ExampleDbContext>(context), IExampleUnitOfWork
{
}
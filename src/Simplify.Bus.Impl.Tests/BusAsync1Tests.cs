using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.Bus.Impl.Tests.TestEntities;

namespace Simplify.Bus.Impl.Tests;

[TestFixture]
public class BusAsync1Tests
{
	[Test]
	public async Task Send_SingleRequestHandler_ExecutedOnceWithCorrectParameter()
	{
		// Arrange

		var request = new TestRequest();

		var handler = new Mock<IRequestHandler<TestRequest>>();

		var bus = new BusAsync<TestRequest>(handler.Object);

		// Act
		await bus.Send(request);

		// Assert

		handler.Verify(x => x.Handle(It.Is<TestRequest>(v => v == request)), Times.Once);
	}
}
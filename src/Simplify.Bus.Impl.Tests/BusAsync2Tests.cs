using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.Bus.Impl.Tests.TestEntities;

namespace Simplify.Bus.Impl.Tests;

[TestFixture]
public class Bus2Async2Tests
{
	[Test]
	public async Task Send_SingleRequestHandler_ExecutedOnceWithCorrectParameterAndResponse()
	{
		// Arrange

		var request = new TestRequest();
		var response = new TestResponse();

		var handler = new Mock<IRequestHandler<TestRequest, TestResponse>>();

		handler.Setup(x => x.Handle(It.Is<TestRequest>(r => r == request))).ReturnsAsync(response);

		var bus = new BusAsync<TestRequest, TestResponse>(handler.Object);

		// Act
		var result = await bus.Send(request);

		// Assert

		handler.Verify(x => x.Handle(It.Is<TestRequest>(r => r == request)), Times.Once);
		Assert.AreEqual(response, result);
	}
}
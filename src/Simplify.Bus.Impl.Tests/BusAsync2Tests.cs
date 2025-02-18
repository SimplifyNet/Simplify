using System.Collections.Generic;
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

		handler
			.Setup(x => x.Handle(It.Is<TestRequest>(r => r == request)))
			.ReturnsAsync(response);

		var bus = new BusAsync<TestRequest, TestResponse>(handler.Object);

		// Act
		var result = await bus.Send(request);

		// Assert

		handler.Verify(x => x.Handle(It.Is<TestRequest>(r => r == request)), Times.Once);
		Assert.That(result, Is.EqualTo(response));
	}

	[Test]
	public async Task Send_TwoBehaviorsWithHandler_ExecutedInOrderWithCorrectTimes()
	{
		// Arrange

		var request = new TestRequest();
		var response = new TestResponse();

		var handler = new Mock<IRequestHandler<TestRequest, TestResponse>>();
		var behavior1 = new Mock<IBehavior<TestRequest, TestResponse>>();
		var behavior2 = new Mock<IBehavior<TestRequest, TestResponse>>();
		var sequence = new MockSequence();

		handler
			.Setup(x => x.Handle(It.Is<TestRequest>(r => r == request)))
			.ReturnsAsync(response);

		behavior1
			.InSequence(sequence)
			.Setup(x => x.Handle(It.IsAny<TestRequest>(), It.IsAny<RequestHandler<TestResponse>>()))
			.Callback((TestRequest request, RequestHandler<TestResponse> next) => next());

		behavior2
			.InSequence(sequence)
			.Setup(x => x.Handle(It.IsAny<TestRequest>(), It.IsAny<RequestHandler<TestResponse>>()))
			.Callback((TestRequest request, RequestHandler<TestResponse> next) => next());

		var bus = new BusAsync<TestRequest, TestResponse>(handler.Object, new List<IBehavior<TestRequest, TestResponse>>
		{
			behavior1.Object,
			behavior2.Object
		});

		// Act
		var result = await bus.Send(request);

		// Assert

		behavior1.Verify(x => x.Handle(It.Is<TestRequest>(v => v == request), It.IsAny<RequestHandler<TestResponse>>()), Times.Once);
		behavior2.Verify(x => x.Handle(It.Is<TestRequest>(v => v == request), It.IsAny<RequestHandler<TestResponse>>()), Times.Once);
		handler.Verify(x => x.Handle(It.Is<TestRequest>(v => v == request)), Times.Once);
	}
}
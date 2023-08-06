using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.Bus.Impl.Tests.TestEntities;

namespace Simplify.Bus.Impl.Tests;

[TestFixture]
public class BusAsync1Tests
{
	[Test]
	public async Task Send_RequestHandler_ExecutedOnceWithCorrectParameter()
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

	[Test]
	public async Task Send_TwoBehaviorsWithHandler_ExecutedInOrderWithCorrectTimes()
	{
		// Arrange
		var request = new TestRequest();

		var handler = new Mock<IRequestHandler<TestRequest>>();
		var behavior1 = new Mock<IBehavior<TestRequest>>();
		var behavior2 = new Mock<IBehavior<TestRequest>>();
		var sequence = new MockSequence();

		behavior1
			.InSequence(sequence)
			.Setup(x => x.Handle(It.IsAny<TestRequest>(), It.IsAny<RequestHandler>()))
			.Callback((TestRequest request, RequestHandler next) => next());

		behavior2
			.InSequence(sequence)
			.Setup(x => x.Handle(It.IsAny<TestRequest>(), It.IsAny<RequestHandler>()))
			.Callback((TestRequest request, RequestHandler next) => next());

		var bus = new BusAsync<TestRequest>(handler.Object, new List<IBehavior<TestRequest>>
		{
			behavior1.Object,
			behavior2.Object
		});

		// Act
		await bus.Send(request);

		// Assert

		behavior1.Verify(x => x.Handle(It.Is<TestRequest>(v => v == request), It.IsAny<RequestHandler>()), Times.Once);
		behavior2.Verify(x => x.Handle(It.Is<TestRequest>(v => v == request), It.IsAny<RequestHandler>()), Times.Once);
		handler.Verify(x => x.Handle(It.Is<TestRequest>(v => v == request)), Times.Once);
	}
}
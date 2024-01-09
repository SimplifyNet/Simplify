using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.Bus.Impl.Tests.TestEntities;

namespace Simplify.Bus.Impl.Tests;

[TestFixture]
public class EventBusAsync1Tests
{
	[Test]
	public async Task Publish_SingleEventHandler_ExecutedOnceWithCorrectParameter()
	{
		// Arrange

		var busEvent = new TestEvent();

		var handler = new Mock<IEventHandler<TestEvent>>();

		var bus = new EventBusAsync<TestEvent>(new List<IEventHandler<TestEvent>> { handler.Object });

		// Act
		await bus.Publish(busEvent);

		// Assert
		handler.Verify(x => x.Handle(It.Is<TestEvent>(v => v == busEvent)), Times.Once);
	}

	[Test]
	public async Task Publish_TwoEventHandlersSyncStopOnExceptionStrategy_ExecutedOnceWithCorrectOrder()
	{
		// Arrange

		var busEvent = new TestEvent();

		var handler1 = new Mock<IEventHandler<TestEvent>>();
		var handler2 = new Mock<IEventHandler<TestEvent>>();
		var sequence = new MockSequence();

		handler1
			.InSequence(sequence)
			.Setup(x => x.Handle(It.Is<TestEvent>(r => r == busEvent)));

		handler2
			.InSequence(sequence)
			.Setup(x => x.Handle(It.Is<TestEvent>(r => r == busEvent)));

		var bus = new EventBusAsync<TestEvent>(new List<IEventHandler<TestEvent>> { handler1.Object, handler2.Object });

		// Act
		await bus.Publish(busEvent);

		// Assert

		handler1.Verify(x => x.Handle(It.Is<TestEvent>(v => v == busEvent)), Times.Once);
		handler2.Verify(x => x.Handle(It.Is<TestEvent>(v => v == busEvent)), Times.Once);
	}

	[Test]
	public void Publish_TwoEventHandlersSyncStopOnExceptionStrategyWithException_ProcessStopped()
	{
		// Arrange

		var busEvent = new TestEvent();

		var handler1 = new Mock<IEventHandler<TestEvent>>();
		var handler2 = new Mock<IEventHandler<TestEvent>>();
		var sequence = new MockSequence();

		handler1
			.InSequence(sequence)
			.Setup(x => x.Handle(It.Is<TestEvent>(r => r == busEvent))).Returns(() => throw new Exception());

		handler2
			.InSequence(sequence)
			.Setup(x => x.Handle(It.Is<TestEvent>(r => r == busEvent)));

		var bus = new EventBusAsync<TestEvent>(new List<IEventHandler<TestEvent>> { handler1.Object, handler2.Object });

		// Act
		Assert.ThrowsAsync<Exception>(async () => await bus.Publish(busEvent));

		// Assert

		handler1.Verify(x => x.Handle(It.Is<TestEvent>(v => v == busEvent)), Times.Once);
		handler2.Verify(x => x.Handle(It.Is<TestEvent>(v => v == busEvent)), Times.Never);
	}

	[Test]
	public async Task Publish_TwoEventHandlersParallelWhenAllStrategy_ExecutedAll()
	{
		// Arrange

		var busEvent = new TestEvent();

		var handler1 = new Mock<IEventHandler<TestEvent>>();
		var handler2 = new Mock<IEventHandler<TestEvent>>();

		handler1.Setup(x => x.Handle(It.Is<TestEvent>(r => r == busEvent)));
		handler2.Setup(x => x.Handle(It.Is<TestEvent>(r => r == busEvent)));

		var bus = new EventBusAsync<TestEvent>(new List<IEventHandler<TestEvent>> { handler1.Object, handler2.Object }, PublishStrategy.ParallelWhenAll);

		// Act
		await bus.Publish(busEvent);

		// Assert

		handler1.Verify(x => x.Handle(It.Is<TestEvent>(v => v == busEvent)), Times.Once);
		handler2.Verify(x => x.Handle(It.Is<TestEvent>(v => v == busEvent)), Times.Once);
	}
}
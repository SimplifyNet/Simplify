using System;
using System.Threading;
using NUnit.Framework;
using Simplify.System.Diagnostics;

namespace Simplify.System.Tests.Diagnostics;

[TestFixture]
public class OffsetStopwatchTests
{
	[Test]
	public void OffsetStopwatch_3HoursAdded_ElapsedTimeWith3Hours()
	{
		// Arrange
		var stopwatch = new OffsetStopwatch(TimeSpan.FromHours(3));

		// Act

		stopwatch.Start();
		Thread.Sleep(2000);
		stopwatch.Stop();

		// Assert

		Assert.That(stopwatch.Elapsed.Hours, Is.EqualTo(3));
		Assert.That(stopwatch.Elapsed.Minutes, Is.EqualTo(0));
		Assert.That(stopwatch.Elapsed.Seconds, Is.EqualTo(2));
	}

	[Test]
	public void OffsetStopwatch_WithoutOffset_ElapsedTimeWithoutOffset()
	{
		// Arrange
		var stopwatch = new OffsetStopwatch();

		// Act

		stopwatch.Start();
		Thread.Sleep(2000);
		stopwatch.Stop();

		// Assert

		Assert.That(stopwatch.Elapsed.Hours, Is.EqualTo(0));
		Assert.That(stopwatch.Elapsed.Minutes, Is.EqualTo(0));
		Assert.That(stopwatch.Elapsed.Seconds, Is.EqualTo(2));
	}
}
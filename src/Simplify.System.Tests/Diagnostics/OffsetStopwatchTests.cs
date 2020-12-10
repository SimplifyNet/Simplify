using System;
using System.Threading;
using NUnit.Framework;
using Simplify.System.Diagnostics;

namespace Simplify.System.Tests.Diagnostics
{
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

			Assert.AreEqual(3, stopwatch.Elapsed.Hours);
			Assert.AreEqual(0, stopwatch.Elapsed.Minutes);
			Assert.AreEqual(2, stopwatch.Elapsed.Seconds);
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

			Assert.AreEqual(0, stopwatch.Elapsed.Hours);
			Assert.AreEqual(0, stopwatch.Elapsed.Minutes);
			Assert.AreEqual(2, stopwatch.Elapsed.Seconds);
		}
	}
}
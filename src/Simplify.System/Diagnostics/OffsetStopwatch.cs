using System;
using System.Diagnostics;

namespace Simplify.System.Diagnostics;

/// <summary>
/// Provides Stopwatch class with elapsed time offset functionality
/// </summary>
public class OffsetStopwatch : Stopwatch
{
	/// <summary>
	/// Initializes a new instance of the <see cref="OffsetStopwatch"/> class.
	/// </summary>
	/// <param name="offset">The timer elapsed time offset.</param>
	public OffsetStopwatch(TimeSpan offset = default)
	{
		Offset = offset;
	}

	/// <summary>
	/// Gets the total elapsed time measured by the current instance.
	/// </summary>
	public new TimeSpan Elapsed => base.Elapsed.Add(Offset);

	/// <summary>
	/// Gets the total elapsed time measured by the current instance, in milliseconds.
	/// </summary>
	public new long ElapsedMilliseconds => base.ElapsedMilliseconds + (long)Offset.TotalMilliseconds;

	/// <summary>
	/// Gets the total elapsed time measured by the current instance, in timer ticks.
	/// </summary>
	public new long ElapsedTicks => base.ElapsedTicks + Offset.Ticks;

	/// <summary>
	/// Gets the offset.
	/// </summary>
	/// <value>
	/// The offset.
	/// </value>
	public TimeSpan Offset { get; private set; }

	/// <summary>
	/// Creates the new timer and starts it immediately.
	/// </summary>
	/// <param name="offset">The offset.</param>
	/// <returns></returns>
	public static OffsetStopwatch StartNew(TimeSpan offset = default)
	{
		var item = new OffsetStopwatch(offset);

		item.Start();

		return item;
	}

	/// <summary>
	/// Stops time interval measurement and resets the elapsed time to zero.
	/// </summary>
	public new void Reset()
	{
		Offset = TimeSpan.Zero;

		base.Reset();
	}

	/// <summary>
	/// Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.
	/// </summary>
	public new void Restart()
	{
		Offset = TimeSpan.Zero;

		base.Restart();
	}
}
using System;

namespace Simplify.System.Extensions;

/// <summary>
/// Provides extensions for the DateTime struct
/// </summary>
public static class DateTimeExtensions
{
	/// <summary>
	/// Removes milliseconds from DateTime
	/// </summary>
	/// <param name="dt">Date and time.</param>
	/// <returns></returns>
	public static DateTime TrimMilliseconds(this DateTime dt)
	{
		// Preserve the original DateTimeKind (Utc/Local/Unspecified); the millisecond-only overload would reset it to Unspecified.
		return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Kind);
	}
}
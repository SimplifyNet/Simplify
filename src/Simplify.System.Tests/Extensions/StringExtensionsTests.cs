using NUnit.Framework;
using Simplify.System.Extensions;

namespace Simplify.System.Tests.Extensions;

[TestFixture]
public class StringExtensionsTests
{
	[Test]
	public void String_ToBytesArray_ConvertedCorrectly()
	{
		Assert.That("test".ToBytesArray(), Is.EqualTo(new byte[] { 116, 0, 101, 0, 115, 0, 116, 0 }));
	}

	[Test]
	public void TryToDateTimeExact_CorrectValue_ConvertedCorrectly()
	{
		// Assign
		const string str = "12.03.13";

		// Act
		var time = str.TryToDateTimeExact("dd.MM.yy");

		// Assert
		Assert.That(time, Is.Not.Null);
		Assert.That(time!.Value.Day, Is.EqualTo(12));
		Assert.That(time.Value.Month, Is.EqualTo(3));
		Assert.That(time.Value.Year, Is.EqualTo(2013));
	}

	[Test]
	public void TryToDateTimeExact_WrongValue_ConvertedCorrectly()
	{
		// Assign
		const string str = "test";

		// Act & Assert
		Assert.That(str.TryToDateTimeExact("dd.MM.yy"), Is.Null);
	}
}

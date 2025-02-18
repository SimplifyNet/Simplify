using System;
using NUnit.Framework;
using Simplify.System.Converters;

// ReSharper disable once ObjectCreationAsStatement

namespace Simplify.System.Tests.Converters;

[TestFixture]
public class ObjectConverterTests
{
	[Test]
	public void AsFunc_Converter_Func()
	{
		// Arrange
		var converter = new ObjectConverter<char, string>(chr => chr.ToString());

		// Act & Assert

		Assert.That(converter.AsFunc(), Is.InstanceOf<Func<char, string>>());
		Assert.That((Func<char, string>)converter, Is.InstanceOf<Func<char, string>>());
	}

	[Test]
	public void Constructor_NullConvertFunc_ThrowsArgumentNullException()
	{
		Assert.That(() => new ObjectConverter<int, int>(null!), Throws.TypeOf<ArgumentNullException>());
	}

	[Test]
	public void Convert_Source_Destination()
	{
		// Arrange
		var converter = new ObjectConverter<string, int>(int.Parse);

		// Act & Assert

		Assert.That(converter.Convert("14"), Is.EqualTo(14));
		Assert.That(converter.Convert("99"), Is.EqualTo(99));
	}
}
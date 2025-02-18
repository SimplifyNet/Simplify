using System;
using NUnit.Framework;
using Simplify.System.Converters;

// ReSharper disable ObjectCreationAsStatement

namespace Simplify.System.Tests.Converters;

[TestFixture]
public class ChainedObjectConverterTests
{
	[Test]
	public void AsFunc_Converter_Func()
	{
		// Arrange
		var converter = new ChainedObjectConverter<object?, string?>(obj => obj?.ToString());

		// Act & Assert

		Assert.That(converter.AsFunc(), Is.InstanceOf<Func<object?, string?>>());
		Assert.That((Func<object?, string?>)converter, Is.InstanceOf<Func<object?, string?>>());
	}

	[Test]
	public void Constructor_NullConvertFunc_ThrowsArgumentNullException()
	{
		Assert.That(() => new ChainedObjectConverter<object, string>(null!), Throws.TypeOf<ArgumentNullException>());
	}

	[Test]
	public void Convert_Source_Destination()
	{
		// Arrange
		var converter = new ChainedObjectConverter<object?, string?>(obj => obj?.ToString());

		// Act & Assert

		Assert.That(converter.Convert(null), Is.EqualTo(null));
		Assert.That(converter.Convert(new object()), Is.EqualTo("System.Object"));
		Assert.That(converter.Convert(new string('A', 5)), Is.EqualTo("AAAAA"));
		Assert.That(converter.Convert("778899"), Is.EqualTo("778899"));
	}

	[Test]
	public void Convert_SourceAndObjectConverter_Destination()
	{
		// Arrange
		var converter1 = new ObjectConverter<object?, string?>(obj => $"{obj?.GetType().Name ?? "NULL"}");
		var converter2 = new ChainedObjectConverter<object?, string?>(obj => $"{obj} : {obj?.ToString()?.Length}", converter1);

		// Act & Assert

		Assert.That(converter2.Convert(null), Is.EqualTo("NULL : 4"));
		Assert.That(converter2.Convert(new object()), Is.EqualTo("Object : 6"));
		Assert.That(converter2.Convert(new DateTime()), Is.EqualTo("DateTime : 8"));
		Assert.That(converter2.Convert("778899"), Is.EqualTo("String : 6"));
	}
}
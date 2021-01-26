using System;
using NUnit.Framework;
using Simplify.System.Converters;

// ReSharper disable ObjectCreationAsStatement

namespace Simplify.System.Tests.Converters
{
	[TestFixture]
	public class ChainedObjectConverterTests
	{
		[Test]
		public void AsFunc_Converter_Func()
		{
			// Arrange
			var converter = new ChainedObjectConverter<object?, string?>(obj => obj?.ToString());

			// Act & Assert

			Assert.IsInstanceOf<Func<object?, string?>>(converter.AsFunc());
			Assert.IsInstanceOf<Func<object?, string?>>((Func<object?, string?>)converter);
		}

		[Test]
		public void Constructor_NullConvertFunc_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new ChainedObjectConverter<object, string>(null!));
		}

		[Test]
		public void Convert_Source_Destination()
		{
			// Arrange
			var converter = new ChainedObjectConverter<object?, string?>(obj => obj?.ToString());

			// Act & Assert

			Assert.AreEqual(converter.Convert(null), null);
			Assert.AreEqual(converter.Convert(new object()), "System.Object");
			Assert.AreEqual(converter.Convert(new string('A', 5)), "AAAAA");
			Assert.AreEqual(converter.Convert("778899"), "778899");
		}

		[Test]
		public void Convert_SourceAndObjectConverter_Destination()
		{
			// Arrange
			var converter1 = new ObjectConverter<object?, string?>(obj => $"{obj?.GetType().Name ?? "NULL"}");
			var converter2 = new ChainedObjectConverter<object?, string?>(obj => $"{obj} : {obj?.ToString()?.Length}", converter1);

			// Act & Assert

			Assert.AreEqual(converter2.Convert(null), "NULL : 4");
			Assert.AreEqual(converter2.Convert(new object()), "Object : 6");
			Assert.AreEqual(converter2.Convert(new DateTime()), "DateTime : 8");
			Assert.AreEqual(converter2.Convert("778899"), "String : 6");
		}
	}
}
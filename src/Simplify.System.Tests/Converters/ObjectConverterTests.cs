using System;
using NUnit.Framework;
using Simplify.System.Converters;

// ReSharper disable once ObjectCreationAsStatement

namespace Simplify.System.Tests.Converters
{
	[TestFixture]
	public class ObjectConverterTests
	{
		[Test]
		public void AsFunc_Converter_Func()
		{
			// Arrange
			var converter = new ObjectConverter<char, string>(chr => chr.ToString());

			// Act & Assert

			Assert.IsInstanceOf<Func<char, string>>(converter.AsFunc());
			Assert.IsInstanceOf<Func<char, string>>((Func<char, string>)converter);
		}

		[Test]
		public void Constructor_NullConvertFunc_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new ObjectConverter<int, int>(null!));
		}

		[Test]
		public void Convert_Source_Destination()
		{
			// Arrange
			var converter = new ObjectConverter<string, int>(int.Parse);

			// Act & Assert

			Assert.AreEqual(converter.Convert("14"), 14);
			Assert.AreEqual(converter.Convert("99"), 99);
		}
	}
}
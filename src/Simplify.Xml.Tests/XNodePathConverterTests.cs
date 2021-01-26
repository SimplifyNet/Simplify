using System;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

// ReSharper disable ConvertToLocalFunction
// ReSharper disable NotAccessedVariable

namespace Simplify.Xml.Tests
{
	[TestFixture]
	public class XNodePathConverterTests
	{
		private readonly XDocument _doc = XDocument.Parse("<root><body><item>Value</item><colors><c>red</c><c>green</c></colors></body></root>");

		[Test]
		public void AsFuncFromInstance_XDocument_ValuesAreEqual()
		{
			// Arrange

			var body = new XNodePathConverter("root/body");
			Func<XNode, XElement> func = x => x.Get("root/body");

			// Act & Assert

			Assert.AreEqual((string)new XNodePathConverter("root/body/item").AsFunc()(_doc), "Value");
			Assert.AreEqual((string)new XNodePathConverter("item", body).AsFunc()(_doc), "Value");
			Assert.AreEqual((string)new XNodePathConverter("item", func).AsFunc()(_doc), "Value");

			Assert.AreEqual((string)new XNodePathConverter("root/body/colors").AsFunc()(_doc).GetMany("c").ElementAt(1), "green");
			Assert.AreEqual((string)new XNodePathConverter("colors", body).AsFunc()(_doc).GetMany("c").ElementAt(0), "red");
			Assert.AreEqual((string)new XNodePathConverter("colors", func).AsFunc()(_doc).GetMany("c").ElementAt(0), "red");
		}

		[Test]
		public void AsFuncStatically_XDocument_ValuesAreEqual()
		{
			// Arrange

			var body = new XNodePathConverter("root/body");
			Func<XNode, XElement> func = x => x.Get("root/body");

			// Act & Assert

			Assert.AreEqual((string)XNodePathConverter.AsFunc("root/body/item")(_doc), "Value");
			Assert.AreEqual((string)XNodePathConverter.AsFunc("item", body)(_doc), "Value");
			Assert.AreEqual((string)XNodePathConverter.AsFunc("item", func)(_doc), "Value");

			Assert.AreEqual((string)XNodePathConverter.AsFunc("root/body/colors")(_doc).GetMany("c").ElementAt(1), "green");
			Assert.AreEqual((string)XNodePathConverter.AsFunc("colors", body)(_doc).GetMany("c").ElementAt(0), "red");
			Assert.AreEqual((string)XNodePathConverter.AsFunc("colors", func)(_doc).GetMany("c").ElementAt(1), "green");
		}

		[Test]
		public void ConvertFromInstance_XDocument_ValuesAreEqual()
		{
			// Arrange

			var body = new XNodePathConverter("root/body");
			Func<XNode, XElement> func = x => x.Get("root/body");

			// Act & Assert

			Assert.AreEqual((string)new XNodePathConverter("root/body/item").Convert(_doc), "Value");
			Assert.AreEqual((string)new XNodePathConverter("item", body).Convert(_doc), "Value");
			Assert.AreEqual((string)new XNodePathConverter("item", func).Convert(_doc), "Value");

			Assert.AreEqual((string)new XNodePathConverter("root/body/colors").Convert(_doc).GetMany("c").ElementAt(1), "green");
			Assert.AreEqual((string)new XNodePathConverter("colors", body).Convert(_doc).GetMany("c").ElementAt(0), "red");
			Assert.AreEqual((string)new XNodePathConverter("colors", func).Convert(_doc).GetMany("c").ElementAt(1), "green");
		}

		[Test]
		public void ConvertStatically_XDocument_ValuesAreEqual()
		{
			// Arrange

			var body = new XNodePathConverter("root/body");
			Func<XNode, XElement> func = x => x.Get("root/body");

			// Act & Assert

			Assert.AreEqual((string)XNodePathConverter.Convert(_doc, "root/body/item"), "Value");
			Assert.AreEqual((string)XNodePathConverter.Convert(_doc, "item", body), "Value");
			Assert.AreEqual((string)XNodePathConverter.Convert(_doc, "item", func), "Value");

			Assert.AreEqual((string)XNodePathConverter.Convert(_doc, "root/body/colors").GetMany("c").ElementAt(0), "red");
			Assert.AreEqual((string)XNodePathConverter.Convert(_doc, "colors", body).GetMany("c").ElementAt(1), "green");
			Assert.AreEqual((string)XNodePathConverter.Convert(_doc, "colors", func).GetMany("c").ElementAt(0), "red");
		}

		[Test]
		public void CreateWithConstructor_XPath_DoesNotThrow()
		{
			// Arrange

			XNodePathConverter body = null;
			XNodePathConverter item = null;

			// Act & Assert

			Assert.DoesNotThrow(() => body = new XNodePathConverter("root/body"));
			Assert.DoesNotThrow(() => item = new XNodePathConverter("item", body));
			Assert.DoesNotThrow(() => item = new XNodePathConverter("item", x => x.Get("root/body")));
		}
	}
}
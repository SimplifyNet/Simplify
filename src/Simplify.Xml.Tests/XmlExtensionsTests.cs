using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using Simplify.Templates;

namespace Simplify.Xml.Tests
{
	[TestFixture]
	public class XmlExtensionsTests
	{
		private const string ExpectedInner = "<foo>data</foo>";
		private const string ExpectedOuter = "<test><foo>data</foo></test>";
		private const string InputString = "<test><foo>data</foo></test>";

		#region Get

		[Test]
		public void GetDescendant_CorrectXPath_ValuesAreEqual()
		{
			// Arrange

			var doc = XDocument.Parse(InputString);

			// Act & Assert

			Assert.AreEqual((string)doc.Root.Get("foo"), "data");
			Assert.AreEqual((string)doc.Get("test/foo"), "data");
		}

		[Test]
		public void GetDescendant_InvalidXPath_Null()
		{
			// Arrange

			var doc = XDocument.Parse(InputString);

			// Act & Assert

			Assert.Null((string)doc.Root.Get("baaa"));
			Assert.Null((string)doc.Get("test/baaa"));
		}

		[Test]
		public void GetDescendant_XNodeIsNull_Null()
		{
			// Arrange

			XDocument doc = null;

			// Act & Assert

			Assert.Null((string)doc.Get("test/foo"));
		}

		[Test]
		public void GetDescendant_XPathIsNullOrWhitespace_ReturnsNull()
		{
			// Arrange

			var doc = XDocument.Parse(InputString);

			// Act & Assert

			Assert.Null((string)doc.Root.Get(null!));
			Assert.Null((string)doc.Root.Get(""));
			Assert.Null((string)doc.Root.Get("  "));
		}

		#endregion Get

		#region GetMany

		[Test]
		public void GetManyDescendants_CorrectXPath_FilledCollection()
		{
			// Arrange

			var doc = XDocument.Parse(InputString);
			var collection = doc.Root.GetMany("foo");

			// Act & Assert

			Assert.IsInstanceOf<IEnumerable<XElement>>(collection);
			Assert.NotNull(collection);
			Assert.IsNotEmpty(collection);
			Assert.AreEqual((string)collection.ElementAt(0), "data");
		}

		[Test]
		public void GetManyDescendants_InvalidXPath_EmptyCollection()
		{
			// Arrange

			var doc = XDocument.Parse(InputString);
			var collection = doc.Root.GetMany("baaaa");

			// Act & Assert

			Assert.IsInstanceOf<IEnumerable<XElement>>(collection);
			Assert.NotNull(collection);
			Assert.IsEmpty(collection);
		}

		[Test]
		public void GetManyDescendants_XNodeIsNull_EmptyCollection()
		{
			// Arrange

			XDocument doc = null;
			// ReSharper disable once ExpressionIsAlwaysNull
			var collection = doc.GetMany("test/foo");

			// Act & Assert

			Assert.IsInstanceOf<IEnumerable<XElement>>(collection);
			Assert.NotNull(collection);
			Assert.IsEmpty(collection);
		}

		[Test]
		public void GetManyDescendants_XPathIsNullOrWhitespace_EmptyCollection()
		{
			// Arrange

			var doc = XDocument.Parse(InputString);

			// Act & Assert

			Assert.IsInstanceOf<IEnumerable<XElement>>(doc.Root.GetMany(null!));
			Assert.NotNull(doc.Root.GetMany(null!));
			Assert.IsEmpty(doc.Root.GetMany(null!));
			Assert.NotNull(doc.Root.GetMany(""));
			Assert.IsEmpty(doc.Root.GetMany(""));
			Assert.NotNull(doc.Root.GetMany("  "));
			Assert.IsEmpty(doc.Root.GetMany("  "));
		}

		#endregion GetMany

		[Test]
		public void GetInnerXml_XElement_GettingCorrectly()
		{
			// Assign
			var element = XElement.Parse(InputString);

			// Act & Assert
			Assert.AreEqual(ExpectedInner, element.InnerXml());
		}

		[Test]
		public void GetOuterXml_XElement_GettingCorrectly()
		{
			// Assign
			var element = XElement.Parse(InputString);

			// Act & Assert
			Assert.AreEqual(ExpectedOuter, element.OuterXml());
		}

		[Test]
		public void RemoveAllXmlNamespaces_XmlStringWithBNamespaces_XmlStringWithoutNamespaces()
		{
			// Assign
			var str = TemplateBuilder.FromCurrentAssembly("TestData.XmlWithNamespaces.xml").Build().Get();

			// Act & Assert
			Assert.AreEqual(TemplateBuilder.FromCurrentAssembly("TestData.XmlWithoutNamespaces..xml").Build().Get(), str.RemoveAllXmlNamespaces());
		}
	}
}
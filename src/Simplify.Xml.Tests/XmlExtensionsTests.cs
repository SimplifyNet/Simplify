using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using Simplify.Templates;

namespace Simplify.Xml.Tests;

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

		Assert.That((string)doc.Root.Get("foo"), Is.EqualTo("data"));
		Assert.That((string)doc.Get("test/foo"), Is.EqualTo("data"));
	}

	[Test]
	public void GetDescendant_InvalidXPath_Null()
	{
		// Arrange

		var doc = XDocument.Parse(InputString);

		// Act & Assert

		Assert.That((string)doc.Root.Get("baaa"), Is.Null);
		Assert.That((string)doc.Get("test/baaa"), Is.Null);
	}

	[Test]
	public void GetDescendant_XNodeIsNull_Null()
	{
		// Arrange

		XDocument doc = null;

		// Act & Assert

		Assert.That((string)doc.Get("test/foo"), Is.Null);
	}

	[Test]
	public void GetDescendant_XPathIsNullOrWhitespace_ReturnsNull()
	{
		// Arrange

		var doc = XDocument.Parse(InputString);

		// Act & Assert

		Assert.That((string)doc.Root.Get(null!), Is.Null);
		Assert.That((string)doc.Root.Get(""), Is.Null);
		Assert.That((string)doc.Root.Get("  "), Is.Null);
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

		Assert.That(collection, Is.InstanceOf<IEnumerable<XElement>>());
		Assert.That(collection, Is.Not.Null);
		Assert.That(collection, Is.Not.Empty);
		Assert.That((string)collection.ElementAt(0), Is.EqualTo("data"));
	}

	[Test]
	public void GetManyDescendants_InvalidXPath_EmptyCollection()
	{
		// Arrange

		var doc = XDocument.Parse(InputString);
		var collection = doc.Root.GetMany("baaaa");

		// Act & Assert

		Assert.That(collection, Is.InstanceOf<IEnumerable<XElement>>());
		Assert.That(collection, Is.Not.Null);
		Assert.That(collection, Is.Empty);
	}

	[Test]
	public void GetManyDescendants_XNodeIsNull_EmptyCollection()
	{
		// Arrange

		XDocument doc = null;
		// ReSharper disable once ExpressionIsAlwaysNull
		var collection = doc.GetMany("test/foo");

		// Act & Assert

		Assert.That(collection, Is.InstanceOf<IEnumerable<XElement>>());
		Assert.That(collection, Is.Not.Null);
		Assert.That(collection, Is.Empty);
	}

	[Test]
	public void GetManyDescendants_XPathIsNullOrWhitespace_EmptyCollection()
	{
		// Arrange

		var doc = XDocument.Parse(InputString);

		// Act & Assert

		Assert.That(doc.Root.GetMany(null!), Is.InstanceOf<IEnumerable<XElement>>());
		Assert.That(doc.Root.GetMany(null!), Is.Not.Null);
		Assert.That(doc.Root.GetMany(null!), Is.Empty);
		Assert.That(doc.Root.GetMany(""), Is.Not.Null);
		Assert.That(doc.Root.GetMany(""), Is.Empty);
		Assert.That(doc.Root.GetMany("  "), Is.Not.Null);
		Assert.That(doc.Root.GetMany("  "), Is.Empty);
	}

	#endregion GetMany

	[Test]
	public void GetInnerXml_XElement_GettingCorrectly()
	{
		// Assign
		var element = XElement.Parse(InputString);

		// Act & Assert
		Assert.That(element.InnerXml(), Is.EqualTo(ExpectedInner));
	}

	[Test]
	public void GetOuterXml_XElement_GettingCorrectly()
	{
		// Assign
		var element = XElement.Parse(InputString);

		// Act & Assert
		Assert.That(element.OuterXml(), Is.EqualTo(ExpectedOuter));
	}

	[Test]
	public void RemoveAllXmlNamespaces_XmlStringWithBNamespaces_XmlStringWithoutNamespaces()
	{
		// Assign
		var str = TemplateBuilder.FromCurrentAssembly("TestData.XmlWithNamespaces.xml").Build().Get();

		// Act & Assert
		Assert.That(str.RemoveAllXmlNamespaces(), Is.EqualTo(TemplateBuilder.FromCurrentAssembly("TestData.XmlWithoutNamespaces..xml").Build().Get()));
	}
}
using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using Simplify.Xml.Tests.TestTypes;

namespace Simplify.Xml.Tests;

[TestFixture]
public class XmlSerializerTests
{
	[Test]
	public void Serialize_TwoItemsWithAssignedProperties_ConvertedToXmlString()
	{
		// Arrange
		var items = new List<FooEntity>
		{
			new()
			{
				ID = 1,
				Name = "Bar1"
			},
			new()
			{
				ID = 2,
				Name = "Bar2"
			}
		};

		// Act
		var result = XmlSerializer.Serialize(items);

		// Assert

		Assert.That(result, Is.EqualTo(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfFooEntity xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FooEntity>
    <ID>1</ID>
    <Name>Bar1</Name>
  </FooEntity>
  <FooEntity>
    <ID>2</ID>
    <Name>Bar2</Name>
  </FooEntity>
</ArrayOfFooEntity>"));
	}

	[Test]
	public void ToXElement_OneItemWithAssignedProperties_EqualToManuallyConstructedXElement()
	{
		// Arrange

		var item = new FooEntity
		{
			ID = 1,
			Name = "Bar1"
		};

		XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
		XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

		var checkObj = new XElement("FooEntity",
			new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
			new XAttribute(XNamespace.Xmlns + "xsd", xsd.NamespaceName),
			new XElement("ID", 1),
			new XElement("Name", "Bar1"));

		// Act
		var result = XmlSerializer.ToXElement(item);

		// Assert

		Assert.That(result.ToString(), Is.EqualTo(checkObj.ToString()));
	}

	[Test]
	public void ToXElement_ValueItemWithAssignedProperties_EqualToManuallyConstructedXElement()
	{
		// Arrange

		var item = new FooValueEntity
		{
			ID = 1,
			Name = "Bar1"
		};

		XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
		XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

		var checkObj = new XElement("FooValueEntity",
			new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
			new XAttribute(XNamespace.Xmlns + "xsd", xsd.NamespaceName),
			new XElement("ID", 1),
			new XElement("Name", "Bar1"));

		// Act
		var result = XmlSerializer.ToXElement(item);

		// Assert

		Assert.That(result.ToString(), Is.EqualTo(checkObj.ToString()));
	}

	[Test]
	public void ToXElement_NullItem_StringRepresentationEqualsToXmLWithNil()
	{
		// Act
		var result = XmlSerializer.ToXElement<FooEntity>(null!);

		// Assert

		Assert.That(result.ToString(), Is.EqualTo("<FooEntity xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xsi:nil=\"true\" />"));
	}

	[Test]
	public void ToXElement_DefaultValueItem_EqualToManuallyWithZeroIntProperty()
	{
		// Arrange

		XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
		XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

		var checkObj = new XElement("FooValueEntity",
			new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
			new XAttribute(XNamespace.Xmlns + "xsd", xsd.NamespaceName),
			new XElement("ID", 0));

		// Act
		var result = XmlSerializer.ToXElement(default(FooValueEntity));

		// Assert

		Assert.That(result.ToString(), Is.EqualTo(checkObj.ToString()));
	}

	[Test]
	public void FromXElement_XElementWithElementsInside_ConvertedObjectPropertiesValuesAreCorrect()
	{
		// Arrange

		XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
		XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

		var checkObj = new XElement("FooEntity",
			new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
			new XAttribute(XNamespace.Xmlns + "xsd", xsd.NamespaceName),
			new XElement("ID", 1),
			new XElement("Name", "Bar1"));

		// Act
		var result = XmlSerializer.FromXElement<FooEntity>(checkObj);

		// Assert

		Assert.That(result.ID, Is.EqualTo(1));
		Assert.That(result.Name, Is.EqualTo("Bar1"));
	}
}
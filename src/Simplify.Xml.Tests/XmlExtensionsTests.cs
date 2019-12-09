using System.Xml.Linq;
using NUnit.Framework;
using Simplify.Templates;

namespace Simplify.Xml.Tests
{
	[TestFixture]
	public class XmlExtensionsTests
	{
		private const string InputString = "<test><foo>data</foo></test>";
		private const string ExpectedOuter = "<test><foo>data</foo></test>";
		private const string ExpectedInner = "<foo>data</foo>";

		[Test]
		public void GetOuterXml_XElement_GettingCorrectly()
		{
			// Assign
			var element = XElement.Parse(InputString);

			// Act & Assert
			Assert.AreEqual(ExpectedOuter, element.OuterXml());
		}

		[Test]
		public void GetInnerXml_XElement_GettingCorrectly()
		{
			// Assign
			var element = XElement.Parse(InputString);

			// Act & Assert
			Assert.AreEqual(ExpectedInner, element.InnerXml());
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
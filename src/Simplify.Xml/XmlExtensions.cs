using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Simplify.Xml;

/// <summary>
/// Provides the extensions for System.Xml.Linq classes
/// </summary>
public static class XmlExtensions
{
	#region Get

	/// <summary>
	/// Gets the descendant element using XPath 1.0 specification
	/// </summary>
	/// <param name="node">The XElement</param>
	/// <param name="xpath">The XPath 1.0 expression</param>
	public static XElement? Get(this XNode? node, string xpath) => node.Get(xpath, null);

	/// <summary>
	/// Gets the descendant element using XPath 1.0 specification
	/// </summary>
	/// <param name="node">The XElement</param>
	/// <param name="xpath">The XPath 1.0 expression</param>
	/// <param name="resolver">The IXmlNamespaceResolver</param>
	public static XElement? Get(this XNode? node, string xpath, IXmlNamespaceResolver? resolver)
	{
		if (node is null || string.IsNullOrWhiteSpace(xpath))
			return null;

		return resolver is null
			? node.XPathSelectElement(xpath)
			: node.XPathSelectElement(xpath, resolver);
	}

	#endregion Get

	#region GetMany

	/// <summary>
	/// Gets the collection of descendant elements using XPath 1.0 specification
	/// </summary>
	/// <param name="node">The XElement</param>
	/// <param name="xpath">The XPath 1.0 expression</param>
	public static IEnumerable<XElement> GetMany(this XNode? node, string xpath) => node.GetMany(xpath, null);

	/// <summary>
	/// Gets the collection of descendant elements using XPath 1.0 specification
	/// </summary>
	/// <param name="node">The XElement</param>
	/// <param name="xpath">The XPath 1.0 expression</param>
	/// <param name="resolver">The IXmlNamespaceResolver</param>
	public static IEnumerable<XElement> GetMany(this XNode? node, string xpath, IXmlNamespaceResolver? resolver)
	{
		if (node is null || string.IsNullOrWhiteSpace(xpath))
			return [];

		return resolver is null
			? node.XPathSelectElements(xpath)
			: node.XPathSelectElements(xpath, resolver);
	}

	#endregion GetMany

	/// <summary>
	/// Gets the inner XML string of an XNode.
	/// </summary>
	/// <param name="element">The inner XML string.</param>
	public static string InnerXml(this XNode element)
	{
		var xReader = element.CreateReader();

		xReader.MoveToContent();

		return xReader.ReadInnerXml();
	}

	/// <summary>
	/// Gets the outer XML string of an XNode (inner XML and itself).
	/// </summary>
	/// <param name="element">The element.</param>
	public static string OuterXml(this XNode element)
	{
		var xReader = element.CreateReader();

		xReader.MoveToContent();

		return xReader.ReadOuterXml();
	}

	/// <summary>
	/// Removes all XML namespaces from string containing XML data.
	/// </summary>
	/// <param name="xmlData">The XML data.</param>
	public static string RemoveAllXmlNamespaces(this string xmlData)
	{
		const string xmlnsPattern = "\\s+xmlns\\s*(:\\w)?\\s*=\\s*\\\"(?<url>[^\\\"]*)\\\"";
		var matchCollection = Regex.Matches(xmlData, xmlnsPattern);

		foreach (var m in matchCollection.Cast<Match?>())
		{
			if (m == null)
				throw new InvalidOperationException("Match collection item is null");

			xmlData = xmlData.Replace(m.ToString(), "");
		}

		return xmlData;
	}
}
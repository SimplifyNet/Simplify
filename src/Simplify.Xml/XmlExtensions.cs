using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Simplify.Xml
{
	/// <summary>
	/// Provides extensions for System.Xml.Linq classes
	/// </summary>
	public static class XmlExtensions
	{
		#region Get

		/// <summary>
		/// Gets the child element using XPath 1.0 specification
		/// </summary>
		/// <param name="node">XElement</param>
		/// <param name="xpath">XPath 1.0</param>
		/// <returns>XElement</returns>
		public static XElement? Get(this XNode? node, string xpath)
		{
			return node.Get(xpath, null);
		}

		/// <summary>
		/// Gets the child element using XPath 1.0 specification
		/// </summary>
		/// <param name="node">XElement</param>
		/// <param name="xpath">XPath 1.0</param>
		/// <param name="resolver">IXmlNamespaceResolver</param>
		/// <returns>XElement</returns>
		public static XElement? Get(this XNode? node, string xpath, IXmlNamespaceResolver? resolver)
		{
			return node is null || string.IsNullOrWhiteSpace(xpath)
				? null
				: resolver is null
					? node.XPathSelectElement(xpath)
					: node.XPathSelectElement(xpath, resolver);
		}

		#endregion Get

		#region GetMany

		/// <summary>
		/// Gets the collection of child elements using XPath 1.0 specification
		/// </summary>
		/// <param name="node">XNode</param>
		/// <param name="xpath">XPath 1.0</param>
		/// <returns>XElement</returns>
		public static IEnumerable<XElement>? GetMany(this XNode? node, string xpath)
		{
			return node.GetMany(xpath, null);
		}

		/// <summary>
		/// Gets the collection of child elements using XPath 1.0 specification
		/// </summary>
		/// <param name="node">XNode</param>
		/// <param name="xpath">XPath 1.0</param>
		/// <param name="resolver">IXmlNamespaceResolver</param>
		/// <returns>XElement</returns>
		public static IEnumerable<XElement>? GetMany(this XNode? node, string xpath, IXmlNamespaceResolver? resolver)
		{
			return node is null || string.IsNullOrWhiteSpace(xpath)
				? null
				: resolver is null
					? node.XPathSelectElements(xpath)
					: node.XPathSelectElements(xpath, resolver);
		}

		#endregion GetMany

		/// <summary>
		/// Gets the inner XML string of an XNode.
		/// </summary>
		/// <param name="element">The inner XML string.</param>
		/// <returns></returns>
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
		/// <returns></returns>
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
		/// <returns></returns>
		public static string RemoveAllXmlNamespaces(this string xmlData)
		{
			const string xmlnsPattern = "\\s+xmlns\\s*(:\\w)?\\s*=\\s*\\\"(?<url>[^\\\"]*)\\\"";
			var matchCollection = Regex.Matches(xmlData, xmlnsPattern);

			foreach (Match? m in matchCollection)
			{
				if (m == null)
					throw new InvalidOperationException("Match collection item is null");

				xmlData = xmlData.Replace(m.ToString(), "");
			}

			return xmlData;
		}
	}
}
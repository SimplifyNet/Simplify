using System;
using System.Xml;
using System.Xml.Linq;
using Simplify.Xml.Converters;

namespace Simplify.Xml;

/// <summary>
/// Provides XNode to XElement converter that resolves xpath by using XPath 1.0
/// </summary>
public class XNodePathConverter : ChainedObjectConverter<XNode?, XElement?>
{
	/// <summary>
	/// XPath 1.0
	/// </summary>
	protected readonly string XPath;

	/// <summary>
	/// IXmlNamespaceResolver
	/// </summary>
	protected readonly IXmlNamespaceResolver? Resolver;

	/// <summary>
	/// Creates instance of XNodePathConverter
	/// </summary>
	/// <param name="xpath">XPath 1.0</param>
	/// <param name="resolver">IXmlNamespaceResolver</param>
	/// <param name="preConvertFunc">Func delegate that converts source to destination before the main converter delegate</param>
	/// <exception cref="ArgumentNullException"></exception>
	public XNodePathConverter(string xpath, IXmlNamespaceResolver? resolver, Func<XNode?, XElement?>? preConvertFunc = null)
		: base(preConvertFunc)
	{
		XPath = xpath;
		Resolver = resolver;
		ConvertFunc = node => node.Get(XPath, Resolver);
	}

	/// <summary>
	/// Creates instance of XNodePathConverter
	/// </summary>
	/// <param name="xpath">XPath 1.0</param>
	/// <param name="preConvertFunc">Func delegate that converts source to destination before the main converter delegate</param>
	public XNodePathConverter(string xpath, Func<XNode?, XElement?>? preConvertFunc = null)
		: this(xpath, null, preConvertFunc)
	{
	}

	/// <summary>
	/// Statically converts XNode to XElement using XPath 1.0
	/// </summary>
	/// <param name="source">XNode source object</param>
	/// <param name="xpath">XPath 1.0</param>
	/// <param name="resolver">IXmlNamespaceResolver</param>
	/// <param name="preConvertFunc">Func delegate that converts source to destination before the main converter delegate</param>
	/// <returns></returns>
	public static XElement? Convert(XNode? source, string xpath, IXmlNamespaceResolver? resolver, Func<XNode?, XElement?>? preConvertFunc = null)
	{
		return new XNodePathConverter(xpath, resolver, preConvertFunc).Convert(source);
	}

	/// <summary>
	/// Statically converts XNode to XElement using XPath 1.0
	/// </summary>
	/// <param name="source">XNode source object</param>
	/// <param name="xpath">XPath 1.0</param>
	/// <param name="preConvertFunc">Func delegate that converts source to destination before the main converter delegate</param>
	/// <returns></returns>
	public static XElement? Convert(XNode? source, string xpath, Func<XNode?, XElement?>? preConvertFunc = null)
	{
		return Convert(source, xpath, null, preConvertFunc);
	}

	/// <summary>
	/// Statically provides Convert method as Func delegate
	/// </summary>
	/// <param name="xpath">XPath 1.0</param>
	/// <param name="resolver">IXmlNamespaceResolver</param>
	/// <param name="preConvertFunc">Func delegate that converts source to destination before the main converter delegate</param>
	/// <returns></returns>
	public static Func<XNode?, XElement?> AsFunc(string xpath, IXmlNamespaceResolver? resolver, Func<XNode?, XElement?>? preConvertFunc = null)
	{
		return new XNodePathConverter(xpath, resolver, preConvertFunc).AsFunc();
	}

	/// <summary>
	/// Statically provides Convert method as Func delegate
	/// </summary>
	/// <param name="xpath">XPath 1.0</param>
	/// <param name="preConvertFunc">Func delegate that converts source to destination before the main converter delegate</param>
	/// <returns></returns>
	public static Func<XNode?, XElement?> AsFunc(string xpath, Func<XNode?, XElement?>? preConvertFunc = null)
	{
		return AsFunc(xpath, null, preConvertFunc);
	}
}
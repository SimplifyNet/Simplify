using System;
using System.Xml;
using System.Xml.Linq;
using Simplify.Xml.Converters;

namespace Simplify.Xml;

/// <summary>
/// Provides the XNode to XElement converter that resolves xpath by using XPath 1.0 expressions
/// </summary>
public class XNodePathConverter : ChainedObjectConverter<XNode?, XElement?>
{
	/// <summary>
	/// The XPath 1.0 expression
	/// </summary>
	protected readonly string XPath;

	/// <summary>
	/// The resolver
	/// </summary>
	protected readonly IXmlNamespaceResolver? Resolver;

	/// <summary>
	/// Creates the instance of XNodePathConverter
	/// </summary>
	/// <param name="xpath">The XPath 1.0 expression</param>
	/// <param name="resolver">The resolver</param>
	/// <param name="preConvertFunc">The Func delegate that converts source to destination before the main converter delegate</param>
	/// <exception cref="ArgumentNullException"></exception>
	public XNodePathConverter(string xpath, IXmlNamespaceResolver? resolver, Func<XNode?, XElement?>? preConvertFunc = null)
		: base(preConvertFunc)
	{
		XPath = xpath;
		Resolver = resolver;
		ConvertFunc = node => node.Get(XPath, Resolver);
	}

	/// <summary>
	/// Creates the instance of XNodePathConverter
	/// </summary>
	/// <param name="xpath">The XPath 1.0 expression</param>
	/// <param name="preConvertFunc">The Func delegate that converts source to destination before the main converter delegate</param>
	public XNodePathConverter(string xpath, Func<XNode?, XElement?>? preConvertFunc = null)
		: this(xpath, null, preConvertFunc)
	{
	}

	/// <summary>
	/// Converts the XNode to XElement using XPath 1.0 expression
	/// </summary>
	/// <param name="source">The XNode source object</param>
	/// <param name="xpath">The XPath 1.0 expression</param>
	/// <param name="resolver">The resolver</param>
	/// <param name="preConvertFunc">The Func delegate that converts source to destination before the main converter delegate</param>
	// ReSharper disable once TooManyArguments
	public static XElement? Convert(XNode? source, string xpath, IXmlNamespaceResolver? resolver, Func<XNode?, XElement?>? preConvertFunc = null) =>
		new XNodePathConverter(xpath, resolver, preConvertFunc).Convert(source);

	/// <summary>
	/// Converts the XNode to XElement using XPath 1.0 expression
	/// </summary>
	/// <param name="source">The XNode source object</param>
	/// <param name="xpath">The XPath 1.0 expression</param>
	/// <param name="preConvertFunc">The Func delegate that converts source to destination before the main converter delegate</param>
	public static XElement? Convert(XNode? source, string xpath, Func<XNode?, XElement?>? preConvertFunc = null) =>
		Convert(source, xpath, null, preConvertFunc);

	/// <summary>
	/// Provides the Convert method as Func delegate
	/// </summary>
	/// <param name="xpath">The XPath 1.0 expression</param>
	/// <param name="resolver">The resolver</param>
	/// <param name="preConvertFunc">The Func delegate that converts source to destination before the main converter delegate</param>
	public static Func<XNode?, XElement?> AsFunc(string xpath, IXmlNamespaceResolver? resolver, Func<XNode?, XElement?>? preConvertFunc = null) =>
		new XNodePathConverter(xpath, resolver, preConvertFunc).AsFunc();

	/// <summary>
	/// Provides the Convert method as Func delegate
	/// </summary>
	/// <param name="xpath">The XPath 1.0 expression</param>
	/// <param name="preConvertFunc">The Func delegate that converts source to destination before the main converter delegate</param>
	public static Func<XNode?, XElement?> AsFunc(string xpath, Func<XNode?, XElement?>? preConvertFunc = null) =>
		AsFunc(xpath, null, preConvertFunc);
}
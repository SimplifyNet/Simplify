using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Simplify.Xml;

namespace Simplify.Templates;

/// <summary>
/// Provides the string table related functionality
/// </summary>
public static class StringTable
{
	/// <summary>
	/// Formats the name of the string table file.
	/// </summary>
	/// <param name="filePath">The file path.</param>
	/// <param name="languageCode">The language code.</param>
	public static string FormatStringTableFileName(string filePath, string languageCode) =>
		$"{filePath}.{languageCode}.xml";

	/// <summary>
	/// Formats the name of the assembly string table file.
	/// </summary>
	/// <param name="filePath">The file path.</param>
	/// <param name="languageCode">The language code.</param>
	public static string FormatAssemblyStringTableFileName(string filePath, string languageCode) =>
		$"{filePath}-{languageCode}.xml";

	/// <summary>
	/// Injects the string table items.
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="stringTable">The string table.</param>
	public static void InjectStringTableItems(ITemplate tpl, IDictionary<string, string?> stringTable)
	{
		foreach (var item in stringTable)
			tpl.Set(item.Key, item.Value);
	}

	/// <summary>
	/// Loads the string table from string.
	/// </summary>
	/// <param name="stringTableXml">The string table XML.</param>
	public static IDictionary<string, string?> LoadStringTableFromString(string stringTableXml)
	{
		var stringTable = XDocument.Parse(stringTableXml);

		if (stringTable.Root != null)
			return stringTable.Root.XPathSelectElements("item[@name]")
				.Where(x => x.HasAttributes)
				.ToDictionary(GetName, GetValue);

		return new Dictionary<string, string?>();
	}

	/// <summary>
	/// Loads the string table from assembly embedded file.
	/// </summary>
	/// <param name="filePath">The string table file path.</param>
	/// <param name="assembly">The assembly.</param>
	public static IDictionary<string, string?> LoadStringTableFromAssembly(string filePath, Assembly assembly) =>
		LoadStringTableFromString(FileReader.ReadFromAssembly(filePath, assembly));

	/// <summary>
	/// Loads the string table from assembly embedded file asynchronously.
	/// </summary>
	/// <param name="filePath">The string table file path.</param>
	/// <param name="assembly">The assembly.</param>
	public static async Task<IDictionary<string, string?>> LoadStringTableFromAssemblyAsync(string filePath, Assembly assembly) =>
		LoadStringTableFromString(await FileReader.ReadFromAssemblyAsync(filePath, assembly));

	/// <summary>
	/// Loads the string table from file.
	/// </summary>
	/// <param name="filePath">The string table file path.</param>
	public static IDictionary<string, string?> LoadStringTableFromFile(string filePath) =>
		LoadStringTableFromString(FileReader.ReadFile(filePath));

	/// <summary>
	/// Loads the string table from file asynchronously.
	/// </summary>
	/// <param name="filePath">The string table file path.</param>
	public static async Task<IDictionary<string, string?>> LoadStringTableFromFileAsync(string filePath) =>
		LoadStringTableFromString(await FileReader.ReadFileAsync(filePath));

	private static string GetName(XElement item) =>
		(string?)item.Attribute("name") ?? throw new InvalidOperationException("name attribute is null");

	private static string? GetValue(XElement item) =>
		string.IsNullOrEmpty(item.Value) ? (string?)item.Attribute("value") : item.InnerXml().Trim();
}
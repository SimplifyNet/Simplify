using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Simplify.Xml;

namespace Simplify.Templates
{
	internal static class StringTable
	{
		internal static string FormatStringTableFileName(string filePath, string languageCode)
		{
			return $"{filePath}.{languageCode}.xml";
		}

		internal static void InjectStringTableItems(ITemplate tpl, IDictionary<string, string> stringTable)
		{
			foreach (var item in stringTable)
				tpl.Set(item.Key, item.Value);
		}

		internal static IDictionary<string, string> LoadStringTableFromFile(string filePath)
		{
			return LoadStringTableFromString(FileReader.ReadFile(filePath));
		}

		internal static async Task<IDictionary<string, string>> LoadStringTableFromFileAsync(string filePath)
		{
			return LoadStringTableFromString(await FileReader.ReadFileAsync(filePath));
		}

		internal static IDictionary<string, string> LoadStringTableFromString(string fileText)
		{
			var stringTable = XDocument.Parse(fileText);

			if (stringTable.Root != null)
				return stringTable.Root.XPathSelectElements("item")
					.Where(x => x.HasAttributes)
					.ToDictionary(GetName, GetValue);

			return new Dictionary<string, string>();
		}

		private static string GetName(XElement item)
		{
			return (string)item.Attribute("name");
		}

		private static string GetValue(XElement item)
		{
			return string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim();
		}
	}
}
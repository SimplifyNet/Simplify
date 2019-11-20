using System.IO;
using System.Threading.Tasks;

namespace Simplify.Templates
{
	internal static class TemplateLocalizeExtensions
	{
		internal static void Localize(this ITemplate tpl, string? filePath, string? language, string? baseLanguage)
		{
			var currentCultureStringTableFilePath = StringTable.FormatStringTableFileName(filePath, language);
			var baseCultureStringTableFilePath = StringTable.FormatStringTableFileName(filePath, baseLanguage);

			if (File.Exists(currentCultureStringTableFilePath))
				StringTable.InjectStringTableItems(tpl, StringTable.LoadStringTableFromFile(currentCultureStringTableFilePath));

			if (File.Exists(baseCultureStringTableFilePath))
				StringTable.InjectStringTableItems(tpl, StringTable.LoadStringTableFromFile(baseCultureStringTableFilePath));

			tpl.Commit();
		}

		internal static async Task LocalizeAsync(this ITemplate tpl, string? filePath, string? language, string? baseLanguage)
		{
			var currentCultureStringTableFilePath = StringTable.FormatStringTableFileName(filePath, language);
			var baseCultureStringTableFilePath = StringTable.FormatStringTableFileName(filePath, baseLanguage);

			if (File.Exists(currentCultureStringTableFilePath))
				StringTable.InjectStringTableItems(tpl, await StringTable.LoadStringTableFromFileAsync(currentCultureStringTableFilePath));

			if (File.Exists(baseCultureStringTableFilePath))
				StringTable.InjectStringTableItems(tpl, await StringTable.LoadStringTableFromFileAsync(baseCultureStringTableFilePath));

			tpl.Commit();
		}
	}
}
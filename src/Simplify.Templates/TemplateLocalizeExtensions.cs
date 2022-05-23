using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Simplify.Templates;

/// <summary>
/// Providers template localization extensions
/// </summary>
public static class TemplateLocalizeExtensions
{
	/// <summary>
	/// Localizes the specified template.
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="filePath">The template file path.</param>
	/// <param name="language">The language.</param>
	/// <param name="baseLanguage">The base language.</param>
	public static void Localize(this ITemplate tpl, string filePath, string language, string baseLanguage)
	{
		var currentCultureStringTableFilePath = StringTable.FormatStringTableFileName(filePath, language);
		var baseCultureStringTableFilePath = StringTable.FormatStringTableFileName(filePath, baseLanguage);

		if (File.Exists(currentCultureStringTableFilePath))
			StringTable.InjectStringTableItems(tpl, StringTable.LoadStringTableFromFile(currentCultureStringTableFilePath));

		if (File.Exists(baseCultureStringTableFilePath))
			StringTable.InjectStringTableItems(tpl, StringTable.LoadStringTableFromFile(baseCultureStringTableFilePath));

		tpl.Commit();
	}

	/// <summary>
	/// Localizes the specified template the asynchronously.
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="filePath">The template file path.</param>
	/// <param name="language">The language.</param>
	/// <param name="baseLanguage">The base language.</param>
	public static async Task LocalizeAsync(this ITemplate tpl, string filePath, string language, string baseLanguage)
	{
		var currentCultureStringTableFilePath = StringTable.FormatStringTableFileName(filePath, language);
		var baseCultureStringTableFilePath = StringTable.FormatStringTableFileName(filePath, baseLanguage);

		if (File.Exists(currentCultureStringTableFilePath))
			StringTable.InjectStringTableItems(tpl, await StringTable.LoadStringTableFromFileAsync(currentCultureStringTableFilePath));

		if (File.Exists(baseCultureStringTableFilePath))
			StringTable.InjectStringTableItems(tpl, await StringTable.LoadStringTableFromFileAsync(baseCultureStringTableFilePath));

		tpl.Commit();
	}

	/// <summary>
	/// Localizes the specified template from the assembly.
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="filePath">The file path.</param>
	/// <param name="assembly">The assembly.</param>
	/// <param name="language">The language.</param>
	/// <param name="baseLanguage">The base language.</param>
	public static void LocalizeFromAssembly(this ITemplate tpl, string filePath, Assembly assembly, string language, string baseLanguage)
	{
		var currentCultureStringTableFilePath = StringTable.FormatAssemblyStringTableFileName(filePath, language);
		var baseCultureStringTableFilePath = StringTable.FormatAssemblyStringTableFileName(filePath, baseLanguage);

		if (FileUtil.AssemblyFileExists(currentCultureStringTableFilePath, assembly))
			StringTable.InjectStringTableItems(tpl, StringTable.LoadStringTableFromAssembly(currentCultureStringTableFilePath, assembly));

		if (FileUtil.AssemblyFileExists(baseCultureStringTableFilePath, assembly))
			StringTable.InjectStringTableItems(tpl, StringTable.LoadStringTableFromAssembly(baseCultureStringTableFilePath, assembly));

		tpl.Commit();
	}

	/// <summary>
	/// Localizes the specified template from the assembly asynchronously.
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="filePath">The file path.</param>
	/// <param name="assembly">The assembly.</param>
	/// <param name="language">The language.</param>
	/// <param name="baseLanguage">The base language.</param>
	public static async Task LocalizeFromAssemblyAsync(this ITemplate tpl, string filePath, Assembly assembly, string language, string baseLanguage)
	{
		var currentCultureStringTableFilePath = StringTable.FormatAssemblyStringTableFileName(filePath, language);
		var baseCultureStringTableFilePath = StringTable.FormatAssemblyStringTableFileName(filePath, baseLanguage);

		if (FileUtil.AssemblyFileExists(currentCultureStringTableFilePath, assembly))
			StringTable.InjectStringTableItems(tpl, await StringTable.LoadStringTableFromAssemblyAsync(currentCultureStringTableFilePath, assembly));

		if (FileUtil.AssemblyFileExists(baseCultureStringTableFilePath, assembly))
			StringTable.InjectStringTableItems(tpl, await StringTable.LoadStringTableFromAssemblyAsync(baseCultureStringTableFilePath, assembly));

		tpl.Commit();
	}
}
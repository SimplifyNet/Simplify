using System.Globalization;

namespace Simplify.Templates;

/// <summary>
/// Template set method extensions
/// </summary>
public static class TemplateSetExtensions
{
	/// <summary>
	/// Set template variable value with text from template (all occurrences will be replaced)
	/// </summary>
	/// <param name="tpl">Value to set</param>
	/// <param name="variableName">Variable name</param>
	/// <param name="template">The template.</param>
	/// <returns></returns>
	public static ITemplate Set(this ITemplate tpl, string variableName, ITemplate template) => tpl.Set(variableName, template?.Get());

	/// <summary>
	/// Set template variable value (all occurrences will be replaced)
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="variableName">Variable name</param>
	/// <param name="value">Value to set</param>
	/// <returns></returns>
	public static ITemplate Set(this ITemplate tpl, string variableName, object? value) => tpl.Set(variableName, value?.ToString());

	/// <summary>
	/// Set template variable value (all occurrences will be replaced)
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="variableName">Variable name</param>
	/// <param name="value">Value to set</param>
	/// <returns></returns>
	public static ITemplate Set(this ITemplate tpl, string variableName, int value) => tpl.Set(variableName, value.ToString(CultureInfo.InvariantCulture));

	/// <summary>
	/// Set template variable value (all occurrences will be replaced)
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="variableName">Variable name</param>
	/// <param name="value">Value to set</param>
	/// <returns></returns>
	public static ITemplate Set(this ITemplate tpl, string variableName, long value) => tpl.Set(variableName, value.ToString(CultureInfo.InvariantCulture));

	/// <summary>
	/// Set template variable value (all occurrences will be replaced)
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="variableName">Variable name</param>
	/// <param name="value">Value to set</param>
	/// <returns></returns>
	public static ITemplate Set(this ITemplate tpl, string variableName, decimal value) => tpl.Set(variableName, value.ToString(CultureInfo.InvariantCulture));

	/// <summary>
	/// Set template variable value (all occurrences will be replaced)
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="variableName">Variable name</param>
	/// <param name="value">Value to set</param>
	/// <returns></returns>
	public static ITemplate Set(this ITemplate tpl, string variableName, double value) => tpl.Set(variableName, value.ToString(CultureInfo.InvariantCulture));
}
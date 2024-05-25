using System.Globalization;

namespace Simplify.Templates;

/// <summary>
/// Provides the template set method extensions
/// </summary>
public static class TemplateSetExtensions
{
	/// <summary>
	/// Sets the template variable value with text from template (all occurrences will be replaced)
	/// </summary>
	/// <param name="tpl">Value to set</param>
	/// <param name="variableName">The variable name</param>
	/// <param name="template">The template.</param>
	public static ITemplate Set(this ITemplate tpl, string variableName, ITemplate template) => tpl.Set(variableName, template?.Get());

	/// <summary>
	/// Sets the template variable value (all occurrences will be replaced)
	/// </summary>
	/// <param name="tpl">The template.</param>
	/// <param name="variableName">The variable name</param>
	/// <param name="value">The value to set</param>
	public static ITemplate Set(this ITemplate tpl, string variableName, object? value) => tpl.Set(variableName, value?.ToString());

	/// <summary>
	/// Sets the template variable value (all occurrences will be replaced)
	/// </summary>
	/// <param name="tpl">The template.</param>
	/// <param name="variableName">The variable name</param>
	/// <param name="value">The value to set</param>
	public static ITemplate Set(this ITemplate tpl, string variableName, int value) => tpl.Set(variableName, value.ToString(CultureInfo.InvariantCulture));

	/// <summary>
	/// Sets the template variable value (all occurrences will be replaced)
	/// </summary>
	/// <param name="tpl">The template.</param>
	/// <param name="variableName">The variable name</param>
	/// <param name="value">The value to set</param>
	public static ITemplate Set(this ITemplate tpl, string variableName, long value) => tpl.Set(variableName, value.ToString(CultureInfo.InvariantCulture));

	/// <summary>
	/// Sets the template variable value (all occurrences will be replaced)
	/// </summary>
	/// <param name="tpl">The template.</param>
	/// <param name="variableName">The variable name</param>
	/// <param name="value">The value to set</param>
	public static ITemplate Set(this ITemplate tpl, string variableName, decimal value) => tpl.Set(variableName, value.ToString(CultureInfo.InvariantCulture));

	/// <summary>
	/// Sets the template variable value (all occurrences will be replaced)
	/// </summary>
	/// <param name="tpl">The template.</param>
	/// <param name="variableName">The variable name</param>
	/// <param name="value">The value to set</param>
	public static ITemplate Set(this ITemplate tpl, string variableName, double value) => tpl.Set(variableName, value.ToString(CultureInfo.InvariantCulture));
}
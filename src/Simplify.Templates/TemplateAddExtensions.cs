using System.Globalization;

namespace Simplify.Templates;

/// <summary>
/// Provides the template add method extensions
/// </summary>
public static class TemplateAddExtensions
{
	/// <summary>
	/// Adds the value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="tpl">The template.</param>
	/// <param name="variableName">Tbe variable name</param>
	/// <param name="value">The value to set</param>
	public static ITemplate Add(this ITemplate tpl, string variableName, int value) => tpl.Add(variableName, value.ToString(CultureInfo.InvariantCulture));

	/// <summary>
	/// Adds the value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="tpl">The template.</param>
	/// <param name="variableName">Tbe variable name</param>
	/// <param name="value">The value to set</param>
	public static ITemplate Add(this ITemplate tpl, string variableName, object? value) => tpl.Add(variableName, value?.ToString());

	/// <summary>
	/// Adds the value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="tpl">The template.</param>
	/// <param name="variableName">Tbe variable name</param>
	/// <param name="value">The value to set</param>
	/// <returns></returns>
	public static ITemplate Add(this ITemplate tpl, string variableName, double value) => tpl.Add(variableName, value.ToString(CultureInfo.InvariantCulture));

	/// <summary>
	/// Adds the value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="tpl">The template.</param>
	/// <param name="variableName">Tbe variable name</param>
	/// <param name="value">The value to set</param>
	public static ITemplate Add(this ITemplate tpl, string variableName, decimal value) => tpl.Add(variableName, value.ToString(CultureInfo.InvariantCulture));

	/// <summary>
	/// Adds the value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="tpl">The template.</param>
	/// <param name="variableName">Tbe variable name</param>
	/// <param name="value">The value to set</param>
	public static ITemplate Add(this ITemplate tpl, string variableName, long value) => tpl.Add(variableName, value.ToString(CultureInfo.InvariantCulture));

	/// <summary>
	/// Adds the value to set template variable value with text from template (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="tpl">The template.</param>
	/// <param name="variableName">Tbe variable name</param>
	/// <param name="template">The value to set</param>
	public static ITemplate Add(this ITemplate tpl, string variableName, ITemplate template) => tpl.Add(variableName, template?.Get());
}
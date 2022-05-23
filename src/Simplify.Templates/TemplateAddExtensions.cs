using System.Globalization;

namespace Simplify.Templates;

/// <summary>
/// Template add method extensions
/// </summary>
public static class TemplateAddExtensions
{
	/// <summary>
	/// Add value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="variableName">Variable name</param>
	/// <param name="value">Value to set</param>
	/// <returns></returns>
	public static ITemplate Add(this ITemplate tpl, string variableName, int value)
	{
		return tpl.Add(variableName, value.ToString(CultureInfo.InvariantCulture));
	}

	/// <summary>
	/// Add value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="variableName">Variable name</param>
	/// <param name="value">Value to set</param>
	/// <returns></returns>
	public static ITemplate Add(this ITemplate tpl, string variableName, object? value)
	{
		return tpl.Add(variableName, value?.ToString());
	}

	/// <summary>
	/// Add value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="variableName">Variable name</param>
	/// <param name="value">Value to set</param>
	/// <returns></returns>
	public static ITemplate Add(this ITemplate tpl, string variableName, double value)
	{
		return tpl.Add(variableName, value.ToString(CultureInfo.InvariantCulture));
	}

	/// <summary>
	/// Add value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="variableName">Variable name</param>
	/// <param name="value">Value to set</param>
	/// <returns></returns>
	public static ITemplate Add(this ITemplate tpl, string variableName, decimal value)
	{
		return tpl.Add(variableName, value.ToString(CultureInfo.InvariantCulture));
	}

	/// <summary>
	/// Add value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="variableName">Variable name</param>
	/// <param name="value">Value to set</param>
	/// <returns></returns>
	public static ITemplate Add(this ITemplate tpl, string variableName, long value)
	{
		return tpl.Add(variableName, value.ToString(CultureInfo.InvariantCulture));
	}

	/// <summary>
	/// Add value to set template variable value with text from template (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="tpl">The TPL.</param>
	/// <param name="variableName">Variable name</param>
	/// <param name="template">Value to set</param>
	/// <returns></returns>
	public static ITemplate Add(this ITemplate tpl, string variableName, ITemplate template)
	{
		return tpl.Add(variableName, template?.Get());
	}
}
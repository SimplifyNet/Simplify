namespace Simplify.Templates.Model;

/// <summary>
/// Provides the model setter base class
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ModelSetterBase" /> class.
/// </remarks>
/// <param name="template">The template.</param>
/// <param name="modelPrefix">The model prefix.</param>
public abstract class ModelSetterBase(ITemplate template, string? modelPrefix = null)
{
	/// <summary>
	/// The model prefix separator
	/// </summary>
	public static string ModelPrefixSeparator { get; set; } = ".";

	/// <summary>
	/// Gets the template.
	/// </summary>
	public ITemplate Template { get; } = template;

	/// <summary>
	/// Gets the model prefix
	/// </summary>
	protected string? ModelPrefix { get; } = modelPrefix;

	/// <summary>
	/// Formats the name of the variable to replace respecting model prefix.
	/// </summary>
	/// <param name="variableName">Name of the variable.</param>
	protected string FormatModelVariableName(string variableName) => ModelPrefix != null ? ModelPrefix + ModelPrefixSeparator + variableName : variableName;
}
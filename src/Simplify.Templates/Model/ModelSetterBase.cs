﻿namespace Simplify.Templates.Model;

/// <summary>
/// Provides ModelSetter base class
/// </summary>
public abstract class ModelSetterBase
{
	/// <summary>
	/// The model prefix separator
	/// </summary>
	public static string ModelPrefixSeparator = ".";

	/// <summary>
	/// Initializes a new instance of the <see cref="ModelSetterBase" /> class.
	/// </summary>
	/// <param name="template">The template.</param>
	/// <param name="modelPrefix">The model prefix.</param>
	protected ModelSetterBase(ITemplate template, string? modelPrefix = null)
	{
		ModelPrefix = modelPrefix;
		Template = template;
	}

	/// <summary>
	/// Gets the template.
	/// </summary>
	/// <value>
	/// The template.
	/// </value>
	public ITemplate Template { get; }

	/// <summary>
	/// The model prefix
	/// </summary>
	protected string? ModelPrefix { get; }

	/// <summary>
	/// Formats the name of the variable to replace respecting model prefix.
	/// </summary>
	/// <param name="variableName">Name of the variable.</param>
	/// <returns></returns>
	protected string FormatModelVariableName(string variableName)
	{
		return ModelPrefix != null ? ModelPrefix + ModelPrefixSeparator + variableName : variableName;
	}
}
using System;
using System.Collections.Generic;

namespace Simplify.Templates;

/// <summary>
/// Provides the text template class
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Template"/> class.
/// </remarks>
/// <param name="initialText">The text.</param>
/// <exception cref="ArgumentNullException">text</exception>
public class Template(string initialText) : ITemplate
{
	private string _text = initialText ?? throw new ArgumentNullException(nameof(initialText));
	private IDictionary<string, string?>? _addValues;

	/// <summary>
	/// Sets the template variable value (all occurrences will be replaced)
	/// </summary>
	/// <param name="variableName">The variable name</param>
	/// <param name="value">The value to set</param>
	public ITemplate Set(string variableName, string? value)
	{
		if (variableName == null)
			throw new ArgumentNullException(nameof(variableName));

		ReplaceWithValue(variableName.Trim(), value);

		return this;
	}

	/// <summary>
	/// Adds the value to the list to set to the template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="variableName">Variable name</param>
	/// <param name="value">Value to set</param>
	/// <exception cref="ArgumentNullException">variableName</exception>
	public ITemplate Add(string variableName, string? value)
	{
		if (variableName == null)
			throw new ArgumentNullException(nameof(variableName));

		_addValues ??= new Dictionary<string, string?>();

		variableName = variableName.Trim();

#if NET48 || NETSTANDARD2_0
		if (!_addValues.ContainsKey(variableName))
			_addValues.Add(variableName, value);
		else
			_addValues[variableName] = _addValues[variableName] + value;
#else
		if (!_addValues.TryAdd(variableName, value))
			_addValues[variableName] += value;
#endif

		return this;
	}

	/// <summary>
	/// Gets the text of the template
	/// </summary>
	public string Get()
	{
		TryReplaceFromAddVariables();

		return _text;
	}

	/// <summary>
	/// Sets the initial template state equal to current.
	/// </summary>
	public void Commit() => initialText = _text;

	/// <summary>
	/// Returns the template to it's initial state
	/// </summary>
	public void RollBack()
	{
		_text = initialText;
		_addValues?.Clear();
	}

	/// <summary>
	/// Gets the text of the template and returns loaded template to it's initial state
	/// </summary>
	public string GetAndRoll()
	{
		var text = Get();

		RollBack();

		return text;
	}

	private void TryReplaceFromAddVariables()
	{
		if (_addValues == null || _addValues.Count == 0)
			return;

		foreach (var addValue in _addValues)
			ReplaceWithValue(addValue.Key, addValue.Value);

		_addValues.Clear();
	}

	private void ReplaceWithValue(string variableName, string? value) => _text = _text.Replace("{" + variableName + "}", value);
}
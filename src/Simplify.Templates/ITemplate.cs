namespace Simplify.Templates;

/// <summary>
/// Represents a text template
/// </summary>
public interface ITemplate
{
	/// <summary>
	/// Sets the  template variable value (all occurrences will be replaced)
	/// </summary>
	/// <param name="variableName">The variable name</param>
	/// <param name="value">The value to set</param>
	ITemplate Set(string variableName, string? value);

	/// <summary>
	/// Adds the value to the list to set to the template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
	/// </summary>
	/// <param name="variableName">Variable name</param>
	/// <param name="value">Value to set</param>
	ITemplate Add(string variableName, string? value);

	/// <summary>
	/// Gets the text of the template
	/// </summary>
	string Get();

	/// <summary>
	/// Sets the initial template state equal to current.
	/// </summary>
	void Commit();

	/// <summary>
	/// Returns the template to it's initial state
	/// </summary>
	void RollBack();

	/// <summary>
	/// Gets the text of the template and returns loaded template to it's initial state
	/// </summary>
	string GetAndRoll();
}
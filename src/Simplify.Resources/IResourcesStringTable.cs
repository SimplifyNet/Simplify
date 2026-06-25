namespace Simplify.Resources;

/// <summary>
/// Interface for getting assembly resource file string
/// </summary>
public interface IResourcesStringTable
{
	/// <summary>
	/// Get string table record by name
	/// </summary>
	string this[string name] { get; }

	/// <summary>
	/// Get string table record by name (returns null if key not found)
	/// </summary>
	string GetString(string name);

	/// <summary>
	/// Get string table record by name, throws KeyNotFoundException if key not found
	/// </summary>
	string GetRequiredString(string name);
}
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Simplify.Resources;

/// <summary>
/// Class for getting assembly resource file string
/// </summary>
public class ResourcesStringTable : IResourcesStringTable
{
	private readonly Assembly _workingAssembly;
	private ResourceManager _resourceManager;

	/// <summary>
	/// Initializes ResourcesStringTable with calling assembly string table
	/// </summary>
	/// <param name="callingAssembly">If true then initializes ResourcesStringTable with calling assembly string table otherwise with entry assembly string table</param>
	/// <param name="resourcesFileName">Resources file name</param>
	/// <param name="baseName">The root name of the resources (Assembly name will be used by default).</param>
	public ResourcesStringTable(bool callingAssembly, string resourcesFileName = "Resources", string baseName = null)
	{
		_workingAssembly = callingAssembly ? Assembly.GetCallingAssembly() : Assembly.GetEntryAssembly();

		InitializeResourceManager(resourcesFileName, baseName);
	}

	/// <summary>
	/// Initializes ResourcesStringTable with specified assembly string table
	/// </summary>
	/// <param name="assembly">Assembly to get string table from</param>
	/// <param name="resourcesFileName">Resources file name</param>
	/// <param name="baseName">The root name of the resources (Assembly name will be used by default).</param>
	public ResourcesStringTable(Assembly assembly, string resourcesFileName = "Resources", string baseName = null)
	{
		_workingAssembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
		InitializeResourceManager(resourcesFileName, baseName);
	}

	/// <summary>
	/// Get string table record by name
	/// </summary>
	public string this[string name] => GetString(name);

	/// <summary>
	/// Get string table record by name
	/// </summary>
	public string GetString(string name) => _resourceManager.GetString(name) ?? throw new KeyNotFoundException($"Resource key '{name}' not found in string table.");

	private void InitializeResourceManager(string resourcesFileName = "Resources", string baseName = null)
	{
		if (_workingAssembly == null)
			throw new InvalidOperationException("Unable to resolve the working assembly for the resources string table (entry assembly is null in the current host). Use the constructor accepting an explicit assembly.");

		if (baseName == null)
			baseName = _workingAssembly.GetName().Name;

		_resourceManager = new ResourceManager(baseName + "." + resourcesFileName, _workingAssembly);
	}
}
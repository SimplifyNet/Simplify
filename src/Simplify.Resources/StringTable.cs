using System;

namespace Simplify.Resources;

/// <summary>
/// Provides application string tables access
/// </summary>
public static class StringTable
{
	private static IResourcesStringTable _entryStringTable;

	private static readonly object EntryLocker = new();

	/// <summary>
	/// Entry assembly string table (ProgramResources.resx)
	/// </summary>
	public static IResourcesStringTable Entry
	{
		get
		{
			if (_entryStringTable != null)
				return _entryStringTable;

			lock (EntryLocker)
				return _entryStringTable ??= new ResourcesStringTable(false, "ProgramResources");
		}
		set => _entryStringTable = value ?? throw new ArgumentNullException(nameof(value));
	}
}
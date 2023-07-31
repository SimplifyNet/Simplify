﻿using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;

namespace Simplify.IO;

/// <summary>
/// Exposes additional static method for files
/// </summary>
public static class FileHelper
{
	private static Lazy<IFileSystem> _fileSystem = new Lazy<IFileSystem>(() => new FileSystem());

	/// <summary>
	/// Gets or sets the file system for Template IO operations.
	/// </summary>
	/// <value>
	/// The file system for Template IO operations.
	/// </value>
	public static IFileSystem FileSystem
	{
		get
		{
			return _fileSystem.Value;
		}

		set
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			_fileSystem = new Lazy<IFileSystem>(() => value);
		}
	}

	/// <summary>
	/// Checking is a file locked for reading or not
	/// </summary>
	/// <param name="filePath">File path</param>
	/// <returns></returns>
	public static bool IsFileLockedForRead(string filePath)
	{
		if (string.IsNullOrEmpty(filePath))
			throw new ArgumentNullException(nameof(filePath));

		if (!FileSystem.File.Exists(filePath))
			throw new FileNotFoundException("File not found: " + filePath);

		var file = new FileInfo(filePath);
		FileStream stream = null;

		try
		{
			stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
		}
		catch (IOException)
		{
			//the file is unavailable because it is:
			//still being written to
			//or being processed by another thread
			//or does not exist (has already been processed)
			return true;
		}
		finally
		{
			stream?.Close();
		}

		//file is not locked
		return false;
	}

	/// <summary>
	/// Return last line of a text file
	/// </summary>
	/// <param name="filePath">File path</param>
	/// <returns></returns>
	public static string GetLastLineOfFile(string filePath)
	{
		if (string.IsNullOrEmpty(filePath))
			throw new ArgumentNullException(nameof(filePath));

		if (!FileSystem.File.Exists(filePath))
			throw new FileNotFoundException("File not found: " + filePath);

		using (var sr = new StreamReader(filePath))
		{
			sr.BaseStream.Seek(0, SeekOrigin.End);

			long pos = -1;

			while (sr.BaseStream.Length + pos > 0)
			{
				sr.BaseStream.Seek(pos, SeekOrigin.End);
				var c = sr.Read();
				sr.DiscardBufferedData();

				if (c == Convert.ToInt32('\n'))
				{
					sr.BaseStream.Seek(pos + 1, SeekOrigin.End);
					return sr.ReadToEnd();
				}

				--pos;
			}
		}

		return null;
	}

	/// <summary>
	/// Replace invalid characters in file name with _
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public static string MakeValidFileName(string name)
	{
		return Path.GetInvalidFileNameChars().Aggregate(name, (current, c) => current.Replace(c, '_'));
	}

	/// <summary>
	/// Generates the full name of file in current directory adding calling assembly path in the start of file name.
	/// </summary>
	/// <param name="fileName">Name of the file.</param>
	/// <returns></returns>
	public static string GenerateFullName(string fileName)
	{
		return $"{Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)}/{fileName}";
	}
}
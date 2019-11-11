using System;
using System.IO;
using System.IO.Abstractions;
using System.Reflection;
using System.Threading.Tasks;

namespace Simplify.Templates
{
	/// <summary>
	/// Provides ITemplate fluent builder
	/// </summary>
	public class TemplateBuilder
	{
		private static Lazy<IFileSystem> _fileSystemInstance = new Lazy<IFileSystem>(() => new FileSystem());

		private string? _text;
		private string? _filePath;

		private TemplateBuilder()
		{
		}

		/// <summary>
		/// Gets or sets the file system for Template IO operations.
		/// </summary>
		/// <value>
		/// The file system for Template IO operations.
		/// </value>
		public static IFileSystem FileSystem
		{
			get => _fileSystemInstance.Value;

			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				_fileSystemInstance = new Lazy<IFileSystem>(() => value);
			}
		}

		/// <summary>
		/// Create builder based on the string.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static TemplateBuilder FromString(string text)
		{
			return new TemplateBuilder
			{
				_text = text
			};
		}

		/// <summary>
		/// Create builder based on the file.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		public static TemplateBuilder FromFile(string filePath)
		{
			return new TemplateBuilder
			{
				_filePath = filePath
			};
		}

		public static TemplateBuilder FromCurrentAssembly(string filePath)
		{
			throw new NotImplementedException();
		}

		public static TemplateBuilder FromAssembly(string filePath, Assembly assembly)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Builds the template.
		/// </summary>
		/// <returns></returns>
		public ITemplate Build()
		{
			BuildProcess();

			if (_text == null)
				throw new TemplateException("Can't initialize empty template");

			return new Template(_text);
		}

		/// <summary>
		/// Builds the template asynchronously.
		/// </summary>
		/// <returns></returns>
		public async Task<ITemplate> BuildAsync()
		{
			await BuildProcessAsync();

			if (_text == null)
				throw new TemplateException("Can't initialize empty template");

			return new Template(_text);
		}

		public TemplateBuilder Localizable(string language, string baseLanguage = "en")
		{
			throw new NotImplementedException();
		}

		public TemplateBuilder LocalizableFromCurrentThreadLanguage(string baseLanguage = "en")
		{
			throw new NotImplementedException();
		}

		public TemplateBuilder FixLineEndingsHtml()
		{
			throw new NotImplementedException();
		}

		private void BuildProcess()
		{
			if (_text != null)
				return;

			if (!string.IsNullOrEmpty(_filePath))
				_text = FileSystem.File.ReadAllText(_filePath);
		}

		private async Task BuildProcessAsync()
		{
			if (_text != null)
				return;

			if (!string.IsNullOrEmpty(_filePath))
				_text = await ReadFileAsync();

			throw new NotImplementedException();
		}

		private Task<string> ReadFileAsync()
		{
			using var stream = FileSystem.FileStream.Create(_filePath, FileMode.Open);

			throw new NotImplementedException();
		}
	}
}
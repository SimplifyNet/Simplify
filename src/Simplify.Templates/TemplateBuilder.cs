using System;
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
		/// Create builder based on a string.
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

		public static TemplateBuilder FromFile(string filePath)
		{
			throw new NotImplementedException();
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
			return new Template(_text);
		}

		public Task<ITemplate> BuildAsync()
		{
			throw new NotImplementedException();
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
	}
}
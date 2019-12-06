using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderTests
	{
		private static readonly object[] _nullFilePathTestCases =
		{
			(TestDelegate) (() => TemplateBuilder.FromCurrentAssembly(null)),
			(TestDelegate) (() => TemplateBuilder.FromAssembly(null, Assembly.GetExecutingAssembly())),
			(TestDelegate) (() => TemplateBuilder.FromLocalFile(null)),
			(TestDelegate) (() => TemplateBuilder.FromFile(null))
		};

		private static readonly object[] _fileNotFoundTestCases =
		{
			(TestDelegate) (() => TemplateBuilder.FromCurrentAssembly("NotFoundFile.txt")),
			(TestDelegate) (() => TemplateBuilder.FromAssembly("NotFoundFile.txt", Assembly.GetExecutingAssembly())),
			(TestDelegate) (() => TemplateBuilder.FromLocalFile("NotFoundFile.txt")),
			(TestDelegate) (() => TemplateBuilder.FromFile("NotFoundFile.txt"))
		};

		[TestCaseSource(nameof(_nullFilePathTestCases))]
		public void NullPath_ArgumentNullException(TestDelegate templateBuilder)
		{
			// Act
			var ex = Assert.Throws<ArgumentException>(templateBuilder);

			// Assert
			Assert.AreEqual("Value cannot be null or empty. (Parameter 'filePath')", ex.Message);
		}

		[TestCaseSource(nameof(_fileNotFoundTestCases))]
		public void FileNotExist_FileNotFoundException(TestDelegate templateBuilder)
		{
			// Act
			var ex = Assert.Throws<FileNotFoundException>(templateBuilder);

			// Assert

			Assert.That(ex.Message, Does.StartWith("Template file not found"));
			Assert.That(ex.Message, Does.EndWith("NotFoundFile.txt"));
		}
	}
}
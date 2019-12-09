using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderFileNotFoundTests
	{
		private const string FilePath = "NotFoundFile.txt";

		private static readonly object[] _testCases =
		{
			(TestDelegate) (() => TemplateBuilder.FromFile(FilePath)),
			(TestDelegate) (() => TemplateBuilder.FromLocalFile(FilePath)),
			(TestDelegate) (() => TemplateBuilder.FromAssembly(FilePath, Assembly.GetExecutingAssembly())),
			(TestDelegate) (() => TemplateBuilder.FromCurrentAssembly(FilePath))
		};

		[TestCaseSource(nameof(_testCases))]
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
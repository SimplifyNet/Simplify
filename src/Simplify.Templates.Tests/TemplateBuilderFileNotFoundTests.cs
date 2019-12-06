using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderFileNotFoundTests
	{
		private static readonly object[] _testCases =
		{
			(TestDelegate) (() => TemplateBuilder.FromAssembly("NotFoundFile.txt", Assembly.GetExecutingAssembly())),
			(TestDelegate) (() => TemplateBuilder.FromCurrentAssembly("NotFoundFile.txt")),
			(TestDelegate) (() => TemplateBuilder.FromFile("NotFoundFile.txt")),
			(TestDelegate) (() => TemplateBuilder.FromLocalFile("NotFoundFile.txt"))
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
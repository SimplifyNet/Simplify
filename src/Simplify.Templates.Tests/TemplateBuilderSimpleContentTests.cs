using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderSimpleContentTests
	{
		private const string EmbeddedTestFilePath = "TestTemplates/Embedded/TestFile.txt";
		//private const string EmbeddedTLocalizationTestFilePath = "TestTemplates/Embedded/LocalizationTest.tpl";

		private const string LocalTestFilePath = "TestTemplates/Local/TestFile.txt";
		//private const string LocalizationTestFilePath = "TestTemplates/Local/LocalizationTest.tpl";

		private static readonly object[] _testCases =
		{
			(TemplateBuilderDelegate) (() => TemplateBuilder.FromAssembly(EmbeddedTestFilePath, Assembly.GetExecutingAssembly())),
			(TemplateBuilderDelegate) (() => TemplateBuilder.FromCurrentAssembly(EmbeddedTestFilePath)),
			(TemplateBuilderDelegate) (() => TemplateBuilder.FromFile(ConstructFullFileName(LocalTestFilePath))),
			(TemplateBuilderDelegate) (() => TemplateBuilder.FromLocalFile(LocalTestFilePath))
		};

		[TestCaseSource(nameof(_testCases))]
		public void Build_TemplateContentIsCorrect(TemplateBuilderDelegate templateBuilder)
		{
			// Arrange
			var builder = templateBuilder();

			// Act
			var tpl = builder.Build();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		[TestCaseSource(nameof(_testCases))]
		public async Task BuildAsync_TemplateContentIsCorrect(TemplateBuilderDelegate templateBuilder)
		{
			// Arrange
			var builder = templateBuilder();

			// Act
			var tpl = await builder.BuildAsync();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		private static string ConstructFullFileName(string filePath)
		{
			return $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{filePath}";
		}
	}
}
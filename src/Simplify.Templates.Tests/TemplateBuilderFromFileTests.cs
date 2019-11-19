using System.Threading.Tasks;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderFromFileTests
	{
		private const string LocalTestFilePath = "TestTemplates/Local/TestFile.txt";

		[SetUp]
		public void Initialize()
		{
		}

		[Test]
		public void Build_FromFile_TemplateGetEqual()
		{
			// Act
			var tpl = TemplateBuilder
				.FromFile(LocalTestFilePath)
				.Build();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		[Test]
		public async Task BuildAsync_FromFile_TemplateGetEqual()
		{
			// Act
			var tpl = await TemplateBuilder
				.FromFile(LocalTestFilePath)
				.BuildAsync();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}
	}
}
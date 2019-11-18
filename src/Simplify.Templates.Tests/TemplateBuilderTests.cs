using System.IO.Abstractions;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderTests
	{
		private Mock<IFileSystem> _fs;

		[SetUp]
		public void Initialize()
		{
			_fs = new Mock<IFileSystem>();
			TemplateBuilder.FileSystem = _fs.Object;
		}

		[Test]
		public void Build_FromString_TemplateGetEqual()
		{
			// Act
			var tpl = TemplateBuilder
				.FromString("test")
				.Build();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		[Test]
		public async Task BuildAsync_FromString_TemplateGetEqual()
		{
			// Act
			var tpl = await TemplateBuilder
				.FromString("test")
				.BuildAsync();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		[Test]
		public void Build_FromFile_TemplateGetEqual()
		{
			// Arrange

			_fs.Setup(x => x.File.ReadAllText(It.Is<string>(s => s == "TestFile.txt"))).Returns("test");

			// Act
			var tpl = TemplateBuilder
				.FromFile("TestFile.txt")
				.Build();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		[Test]
		public async Task BuildAsync_FromFile_TemplateGetEqual()
		{
			// Arrange

			_fs.Setup(x => x.File.ReadAllText(It.Is<string>(s => s == "TestFile.txt"))).Returns("test");

			// Act
			var tpl = await TemplateBuilder
				.FromFile("TestFile.txt")
				.BuildAsync();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}
	}
}
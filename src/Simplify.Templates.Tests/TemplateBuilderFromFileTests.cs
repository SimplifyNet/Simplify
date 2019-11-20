using System;
using System.IO;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderFromFileTests
	{
		[Test]
		public void FromFile_NullPath_ArgumentNullException()
		{
			// Act
			var ex = Assert.Throws<ArgumentException>(() => TemplateBuilder.FromLocalFile(null));

			// Assert
			Assert.AreEqual("Value cannot be null or empty.\r\nParameter name: filePath", ex.Message);
		}

		[Test]
		public void FromFile_FileNotExist_FileNotFoundException()
		{
			// Act
			var ex = Assert.Throws<FileNotFoundException>(() => TemplateBuilder.FromFile("NotFoundFile.txt"));

			// Assert

			Assert.That(ex.Message, Does.StartWith("Template file not found"));
			Assert.That(ex.Message, Does.EndWith("NotFoundFile.txt"));
		}
	}
}
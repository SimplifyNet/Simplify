using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderFromStringTests
	{
		[Test]
		public void FromString_NullString_TemplateGetEqual()
		{
			// Act
			var ex = Assert.Throws<ArgumentNullException>(() => TemplateBuilder.FromString(null));

			// Assert
			Assert.AreEqual("Value cannot be null. (Parameter 'text')", ex.Message);
		}

		[Test]
		public void Build_FromString_TemplateContentIsCorrect()
		{
			// Act
			var tpl = TemplateBuilder
				.FromString("test")
				.Build();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		[Test]
		public async Task BuildAsync_FromString_TemplateContentIsCorrect()
		{
			// Act
			var tpl = await TemplateBuilder
				.FromString("test")
				.BuildAsync();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		[Test]
		public void FromStringFixLineEndingsHtml_WithLineBreak_LineEndingReplacedWithBrs()
		{
			// Act
			var tpl = TemplateBuilder
				.FromString("test\r\ntest2")
				.FixLineEndingsHtml()
				.Build();

			// Assert
			Assert.AreEqual("test<br />test2", tpl.Get());
		}
	}
}
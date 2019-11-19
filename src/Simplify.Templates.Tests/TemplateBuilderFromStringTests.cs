using System.Threading.Tasks;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderFromStringTests
	{
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
	}
}
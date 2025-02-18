using NUnit.Framework;
using Simplify.System.Extensions;

namespace Simplify.System.Tests.Extensions;

[TestFixture]
public class DoubleExtensionsTests
{
	[Test]
	public void Double_CompareTwoDoubles_ComparedCorrectly()
	{
		Assert.That(15.27.AreSameAs(15.27), Is.True);
		Assert.That(155656564.272323231123.AreSameAs(155656564.272323231123), Is.True);
		Assert.That(155656564.2723232311.AreSameAs(155656564.2723232), Is.False);
	}
}
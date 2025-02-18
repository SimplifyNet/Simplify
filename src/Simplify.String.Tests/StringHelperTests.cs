using System;

using NUnit.Framework;

namespace Simplify.String.Tests;

[TestFixture]
public class StringHelperTests
{
	[Test]
	public void IsMobilePhoneParsingCorrectly()
	{
		Assert.That(StringHelper.ParseMobilePhone("+7 701 22 56 245"), Is.EqualTo("+77012256245"));
		Assert.That(StringHelper.ParseMobilePhone("+7701 2256 245"), Is.EqualTo("+77012256245"));
		Assert.That(StringHelper.ParseMobilePhone("7-701-22-56-245"), Is.EqualTo("77012256245"));
		Assert.That(StringHelper.ParseMobilePhone("8(701)2256245"), Is.EqualTo("87012256245"));
	}

	[Test]
	public void IsEMailValidatingCorrectly()
	{
		Assert.That(StringHelper.ValidateEMail("testname@pupkin.org"), Is.True);
		Assert.That(StringHelper.ValidateEMail("someaddress.test.company@pupkin.org"), Is.True);
		Assert.That(StringHelper.ValidateEMail("someaddress.test@company"), Is.True);
		Assert.That(StringHelper.ValidateEMail("someaddress.test@companyorg"), Is.True);

		Assert.That(StringHelper.ValidateEMail(null), Is.False);
		Assert.That(StringHelper.ValidateEMail("someaddress"), Is.False);
		Assert.That(StringHelper.ValidateEMail("someaddress.test.company.org"), Is.False);
		Assert.That(StringHelper.ValidateEMail("someaddress@test@company.org"), Is.False);
		Assert.That(StringHelper.ValidateEMail("someaddress.test..company.org"), Is.False);
	}

	[Test]
	public void IsMobilePhoneValid()
	{
		Assert.That(StringHelper.ValidateMobilePhone("+77015634321"), Is.True);
		Assert.That(StringHelper.ValidateMobilePhone("+7015634321"), Is.True);
		Assert.That(StringHelper.ValidateMobilePhone("+666567890345"), Is.True);
		Assert.That(StringHelper.ValidateMobilePhone("+9944511122333"), Is.True);
		Assert.That(StringHelper.ValidateMobilePhone("+9941234"), Is.True);

		Assert.That(StringHelper.ValidateMobilePhone(null), Is.False);
		Assert.That(StringHelper.ValidateMobilePhone("8 (701) 56 34 321"), Is.False);
		Assert.That(StringHelper.ValidateMobilePhone("+7701563432A"), Is.False);
		Assert.That(StringHelper.ValidateMobilePhone("+7701563432*"), Is.False);
	}

	[Test]
	public void IsIndistinctMatchingMatchCorrectly()
	{
		Assert.That(StringHelper.IndistinctMatching("asdfghjkl", "fghasdjkl"), Is.EqualTo(75));
		Assert.That(StringHelper.IndistinctMatching("asdfghjkl", "asdfghjkl"), Is.EqualTo(100));
		Assert.That(StringHelper.IndistinctMatching("qwerty", "asdfgh"), Is.EqualTo(0));
		Assert.That(StringHelper.IndistinctMatching("qwerty", "ytrewq"), Is.EqualTo(40));
		Assert.That(Math.Round(StringHelper.IndistinctMatching("qwe asd", "asd qwe"), 2), Is.EqualTo(72.22));
		Assert.That(StringHelper.IndistinctMatching("qweasdzxc", "qwezxcasd"), Is.EqualTo(75));
		Assert.That(Math.Round(StringHelper.IndistinctMatching("qweasdzxc", "qweasdzxx"), 2), Is.EqualTo(89.58));
		Assert.That(StringHelper.IndistinctMatching(null, "fghasdjkl"), Is.EqualTo(0));
		Assert.That(StringHelper.IndistinctMatching("a", "fghasdjkl", 0), Is.EqualTo(0));
	}

	[Test]
	public void IsStripHtmlTagsCorrectly()
	{
		Assert.That(StringHelper.StripHtmlTags("<a href=\"link\">text</a><input name=\"login\" />"), Is.EqualTo("text"));
	}
}
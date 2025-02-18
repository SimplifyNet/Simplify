using System;
using System.IO;
using NUnit.Framework;

namespace Simplify.IO.Tests;

[TestFixture]
public class FileHelperTester
{
	//[Test]
	//public void IsGetLastLineOfFileGettingCorrectly()
	//{
	//	Assert.That(FileHelper.GetLastLineOfFile("TestData/Local/MasterTemplate.tpl"), Is.EqualTo("</div>"));
	//	Assert.That(FileHelper.GetLastLineOfFile("TestData/EmptyFile.txt"), Is.Null);
	//	Assert.That(() => FileHelper.GetLastLineOfFile(null), Throws.TypeOf<ArgumentNullException>());
	//	Assert.That(() => FileHelper.GetLastLineOfFile("NotFound"), Throws.TypeOf<FileNotFoundException>());
	//}

	[Test]
	public void IsFileLockedForRead()
	{
		Assert.That(() => FileHelper.IsFileLockedForRead(null), Throws.TypeOf<ArgumentNullException>());
		Assert.That(() => FileHelper.IsFileLockedForRead("NotFound"), Throws.TypeOf<FileNotFoundException>());
	}

	[Test]
	[Category("Windows")]
	public void IsFileNameMadeValidCorrectly()
	{
		Assert.That(FileHelper.MakeValidFileName(@"thisIsValid.txt"), Is.EqualTo("thisIsValid.txt"));

		Assert.That(FileHelper.MakeValidFileName(@"thisIsNotValid\3\\_3.txt"), Is.EqualTo("thisIsNotValid_3___3.txt"));
		Assert.That(FileHelper.MakeValidFileName(@"thisIsNotValid.t\xt"), Is.EqualTo("thisIsNotValid.t_xt"));
		Assert.That(FileHelper.MakeValidFileName(@"testfile: do?.txt"), Is.EqualTo("testfile_ do_.txt"));
	}
}
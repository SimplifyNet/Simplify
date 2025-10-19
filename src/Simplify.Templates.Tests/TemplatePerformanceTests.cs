using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Simplify.Templates.Tests;

[TestFixture]
[Category("Integration")]
public class TemplatePerformanceTests
{
	[Test]
	public void MeasurePerformance_MultipleReplacements()
	{
		// Arrange

		const int iterations = 1000000;

		var sw = new Stopwatch();

		// Force GC collect before test

		GC.Collect();
		GC.WaitForPendingFinalizers();

		var memoryBefore = GC.GetTotalMemory(true);

		// Act

		sw.Start();

		for (var i = 0; i < iterations; i++)
		{
			var tpl = TemplateBuilder
				.FromCurrentAssembly("TestTemplates/Embedded/PerformanceTemplate.tpl")
				.Build();

			var checkData = TemplateBuilder
				.FromCurrentAssembly("TestTemplates/Embedded/PerformanceTemplateResult.tpl")
				.Build()
				.Get();

			tpl.Set("var1", "test1")
				.Set("var2", "test2")
				.Set("var3", "test3")
				.Set("var4", "test4")
				.Set("var5", "test5")
				.Get();

			Assert.That(tpl.Get(), Is.EqualTo(checkData));
		}

		sw.Stop();

		// Measure memory after

		var memoryAfter = GC.GetTotalMemory(true);

		GC.Collect();
		GC.WaitForPendingFinalizers();

		// Assert & Report

		var memoryUsed = memoryAfter - memoryBefore;

		TestContext.WriteLine($"Time taken for {iterations} iterations: {sw.ElapsedMilliseconds}ms");
		TestContext.WriteLine($"Memory before: {memoryBefore / 1024}KB");
		TestContext.WriteLine($"Memory after: {memoryAfter / 1024}KB");
		TestContext.WriteLine($"Memory difference: {memoryUsed / 1024}KB");
		TestContext.WriteLine($"Average memory per iteration: {(memoryUsed / iterations):N2} bytes");
	}
}

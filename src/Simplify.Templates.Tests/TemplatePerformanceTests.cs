using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Simplify.Templates.Tests;

[TestFixture]
public class TemplatePerformanceTests
{
	[Test]
	public void MeasurePerformance_MultipleReplacements()
	{
		// Arrange

		const int iterations = 1000000;

		var template = @"Lorem ipsum dolor sit amet {var1} consectetur adipiscing elit {var2}, 
            sed do eiusmod tempor incididunt {var3} ut labore et dolore magna aliqua. 
            Ut enim ad minim veniam {var4}, quis nostrud exercitation ullamco {var5} 
            laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in {var1} 
            reprehenderit in voluptate {var2} velit esse cillum dolore eu fugiat {var3} 
            nulla pariatur. Excepteur sint occaecat {var4} cupidatat non proident {var5}, 
            sunt in culpa qui officia deserunt {var1} mollit anim id est laborum {var2}. 
            Multiple replacements: {var3} {var4} {var5} {var1} {var2} {var3} {var4} {var5}";

		var sw = new Stopwatch();

		// Force GC collect before test

		GC.Collect();
		GC.WaitForPendingFinalizers();

		var memoryBefore = GC.GetTotalMemory(true);

		// Act

		sw.Start();

		for (var i = 0; i < iterations; i++)
		{
			var testData = TemplateBuilder
				.FromString(template)
				.Build()
				.Set("var1", "test1")
				.Set("var2", "test2")
				.Set("var3", "test3")
				.Set("var4", "test4")
				.Set("var5", "test5")
				.Get();
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

using System;

namespace Simplify.Pipelines.Processing
{
	[Obsolete("Please use IConveyor with exceptions")]
	/// <summary>
	/// Provides pipeline related action delegate
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="item">The item.</param>
	public delegate void PipelineAction<in T>(T item);
}
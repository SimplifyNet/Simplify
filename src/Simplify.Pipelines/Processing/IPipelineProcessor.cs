using System;

namespace Simplify.Pipelines.Processing;

/// <summary>
/// Represent pipeline processor
/// </summary>
[Obsolete("Please use IConveyor with exceptions")]
public interface IPipelineProcessor
{
	/// <summary>
	/// Executes pipeline.
	/// </summary>
	void Execute();
}
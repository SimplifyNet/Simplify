using System;

namespace Simplify.Pipelines.Processing;

/// <summary>
/// Represent pipeline with processing error result information
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
[Obsolete("Please use IConveyor with exceptions")]
public interface IResultingPipeline<in T, out TResult>
{
	/// <summary>
	/// Gets the error result.
	/// </summary>
	/// <value>
	/// The error result.
	/// </value>
	TResult ErrorResult { get; }

	/// <summary>
	/// Process item through pipeline.
	/// </summary>
	/// <param name="item">The item for execution.</param>
	/// <returns></returns>
	bool Execute(T item);
}
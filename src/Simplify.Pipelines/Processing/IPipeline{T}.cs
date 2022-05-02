namespace Simplify.Pipelines.Processing
{
	[Obsolete("Please use IConveyor with exceptions")]
	/// <summary>
	/// Represent pipeline
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPipeline<T>
	{
		/// <summary>
		/// Occurs when pipeline is about to execute.
		/// </summary>
		event PipelineAction<T> OnPipelineStart;

		/// <summary>
		/// Occurs when pipeline has finished it's execution.
		/// </summary>
		event PipelineAction<T> OnPipelineEnd;

		/// <summary>
		/// Occurs when pipeline stage has finished it's execution.
		/// </summary>
		event PipelineStageAction<T> OnStageExecuted;

		/// <summary>
		/// Process item through pipeline.
		/// </summary>
		/// <param name="item">The item for execution.</param>
		/// <returns></returns>
		bool Execute(T item);
	}
}
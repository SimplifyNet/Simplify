using System.Collections.Generic;

namespace Simplify.Pipelines.Processing
{
	/// <summary>
	/// Provides default resulting pipeline
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <seealso cref="IResultingPipeline{T, TResult}" />
	public class ResultingPipeline<T, TResult> : IResultingPipeline<T, TResult>
	{
		private readonly IList<IResultingPipelineStage<T, TResult>> _stages;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResultingPipeline{T, TResult}"/> class.
		/// </summary>
		/// <param name="stages">The stages.</param>
		public ResultingPipeline(IList<IResultingPipelineStage<T, TResult>> stages)
		{
			_stages = stages;
		}

		/// <summary>
		/// Occurs when pipeline has finished it's execution.
		/// </summary>
		public event PipelineAction<T> OnPipelineEnd;

		/// <summary>
		/// Occurs when pipeline is about to execute.
		/// </summary>
		public event PipelineAction<T> OnPipelineStart;

		/// <summary>
		/// Occurs when pipeline stage has finished it's execution.
		/// </summary>
		public event PipelineStageAction<T> OnStageExecuted;

		/// <summary>
		/// Gets the error result.
		/// </summary>
		/// <value>
		/// The error result.
		/// </value>
		public TResult ErrorResult { get; private set; }

		/// <summary>
		/// Process pipeline stages.
		/// </summary>
		/// <param name="item">The item for execution.</param>
		/// <returns></returns>
		public bool Execute(T item)
		{
			OnPipelineStart?.Invoke(item);

			foreach (var stage in _stages)
			{
				var result = stage.Execute(item);

				OnStageExecuted?.Invoke(stage.GetType(), item, result);

				if (result)
					continue;

				ErrorResult = stage.ErrorResult;

				return false;
			}

			OnPipelineEnd?.Invoke(item);

			return true;
		}
	}
}
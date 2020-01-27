using System.Collections.Generic;

namespace Simplify.Pipelines.Processing
{
	/// <summary>
	/// Provides default pipeline
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="IPipeline{T}" />
	public class Pipeline<T> : IPipeline<T>
	{
		private readonly IList<IPipelineStage<T>> _stages;

		/// <summary>
		/// Initializes a new instance of the <see cref="Pipeline{T}"/> class.
		/// </summary>
		/// <param name="stages">The pipeline stages.</param>
		public Pipeline(IList<IPipelineStage<T>> stages)
		{
			_stages = stages;
		}

		/// <summary>
		/// Occurs when pipeline is about to execute.
		/// </summary>
		public event PipelineAction<T> OnPipelineStart;

		/// <summary>
		/// Occurs when pipeline has finished it's execution.
		/// </summary>
		public event PipelineAction<T> OnPipelineEnd;

		/// <summary>
		/// Occurs when pipeline stage has finished it's execution.
		/// </summary>
		public event PipelineStageAction<T> OnStageExecuted;

		/// <summary>
		/// Process pipeline stages.
		/// </summary>
		/// <param name="item">The item for execution.</param>
		/// <returns></returns>
		public virtual bool Execute(T item)
		{
			OnPipelineStart?.Invoke(item);

			foreach (var stage in _stages)
			{
				var result = !stage.Execute(item);

				OnStageExecuted?.Invoke(stage.GetType(), item, result);

				if (result == false)
					return false;
			}

			OnPipelineEnd?.Invoke(item);

			return true;
		}
	}
}
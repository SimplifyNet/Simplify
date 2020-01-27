using System;
using System.Collections.Generic;

namespace Simplify.Pipelines.ProductionLine
{
	/// <summary>
	/// Provides default conveyor
	/// </summary>
	/// <typeparam name="T">Conveyor item type</typeparam>
	/// <seealso cref="IConveyor{T}" />
	public class Conveyor<T> : IConveyor<T>
	{
		private readonly IList<IConveyorStage<T>> _stages;

		/// <summary>
		/// Initializes a new instance of the <see cref="Conveyor{T}"/> class.
		/// </summary>
		/// <param name="stages">The stages.</param>
		/// <exception cref="ArgumentNullException">stages</exception>
		public Conveyor(IList<IConveyorStage<T>> stages)
		{
			_stages = stages ?? throw new ArgumentNullException(nameof(stages));
		}

		/// <summary>
		/// Occurs when conveyor is about to execute.
		/// </summary>
		public event ConveyorAction<T> OnConveyorStart;

		/// <summary>
		/// Occurs when conveyor stage has finished it's execution.
		/// </summary>
		public event ConveyorStageAction<T> OnStageExecuted;

		/// <summary>
		/// Executes the specified item thru conveyor.
		/// </summary>
		/// <param name="item">The item.</param>
		public void Execute(T item)
		{
			OnConveyorStart?.Invoke(item);

			foreach (var stage in _stages)
			{
				stage.Execute(item);

				OnStageExecuted?.Invoke(stage.GetType(), item);
			}
		}
	}
}
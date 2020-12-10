namespace Simplify.Pipelines.ProductionLine
{
	/// <summary>
	/// Represent conveyor
	/// </summary>
	/// <typeparam name="T">Conveyor item type</typeparam>
	public interface IConveyor<T>
	{
		/// <summary>
		/// Occurs when conveyor is about to execute.
		/// </summary>
		event ConveyorAction<T> OnConveyorStart;

		/// <summary>
		/// Occurs when conveyor stage has finished it's execution.
		/// </summary>
		event ConveyorStageAction<T> OnStageExecuted;

		/// <summary>
		/// Executes the specified item thru conveyor.
		/// </summary>
		/// <param name="item">The item.</param>
		void Execute(T item);
	}
}
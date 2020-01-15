namespace Simplify.Pipelines.ProductionLine
{
	/// <summary>
	/// Represent conveyor
	/// </summary>
	/// <typeparam name="T">Conveyor item type</typeparam>
	public interface IConveyor<in T>
	{
		/// <summary>
		/// Executes the specified item thru conveyor.
		/// </summary>
		/// <param name="item">The item.</param>
		void Execute(T item);
	}
}
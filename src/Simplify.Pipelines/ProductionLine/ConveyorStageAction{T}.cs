using System;

namespace Simplify.Pipelines.ProductionLine
{
	/// <summary>
	/// Provides conveyor stage action delegate
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="stageType">Type of the stage.</param>
	/// <param name="item">The item.</param>
	public delegate void ConveyorStageAction<in T>(Type stageType, T item);
}
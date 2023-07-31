namespace Simplify.Bus;

public enum PublishStrategy
{
	SyncStopOnException = 0,

	ParallelWhenAll = 1
}
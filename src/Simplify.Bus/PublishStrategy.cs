using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplify.Bus;

public enum PublishStrategy
{
	SyncStopOnException = 0,

	ParallelWhenAll = 1
}

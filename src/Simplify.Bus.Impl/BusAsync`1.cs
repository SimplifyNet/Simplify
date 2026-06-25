using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Simplify.Bus.Process;

namespace Simplify.Bus.Impl;

public class BusAsync<TRequest>(IRequestHandler<TRequest> requestHandler, IReadOnlyList<IBehavior<TRequest>>? behaviors = null) : IBusAsync<TRequest>
{
	private readonly IRequestHandler<TRequest> _requestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));

	public Task Send(TRequest request)
	{
		var behavior = behaviors.WrapUp(_requestHandler);

		return behavior != null
			 ? behavior.Handle(request)
			 : _requestHandler.Handle(request);
	}
}
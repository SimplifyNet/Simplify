using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Simplify.Bus.Process;

namespace Simplify.Bus.Impl;

public class BusAsync<TRequest, TResponse>(IRequestHandler<TRequest, TResponse> requestHandler,
	IReadOnlyList<IBehavior<TRequest, TResponse>>? behaviors = null) : IBusAsync<TRequest, TResponse>
{
	private readonly IRequestHandler<TRequest, TResponse> _requestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));

	public Task<TResponse> Send(TRequest request)
	{
		var behavior = behaviors.WrapUp(_requestHandler);

		return behavior != null
			 ? behavior.Handle(request)
			 : _requestHandler.Handle(request);
	}
}
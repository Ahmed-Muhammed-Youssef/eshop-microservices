using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[STRAT] Handle request={Request} - response={Response} - request data = {RequestData}", typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            
            timer.Start();

            var response = await next();

            timer.Stop();

            var timeTaken = timer.Elapsed;

            if(timeTaken.Seconds > 3)
            {
                _logger.LogWarning("[PERFORMANCE] the request {Request} took {TimeTaken}", typeof(TRequest).Name, timeTaken.Seconds);
            }

            _logger.LogInformation("[END] Handled {Request} With {Response}", typeof(TRequest).Name, response);

            return response;
        }
    }
}

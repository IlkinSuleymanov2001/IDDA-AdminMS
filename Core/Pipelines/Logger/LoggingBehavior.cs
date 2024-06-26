using MediatR;
namespace Core.Pipelines.Logger;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IBaseRequest
{
    private readonly Logger _logger;

    public LoggingBehavior(Logger logger)
    {
        _logger = logger;
    }

    public async  Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex) when(ex is not INonLogException)
        {
            _logger.LogErrorToPostgreDatabase(ex);
              throw;
        }


     }
 
}






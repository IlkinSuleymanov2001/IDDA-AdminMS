using MediatR;
using System.Transactions;

namespace Core.Pipelines.Transaction;

public class TransactionBehavior<TRequest, TResponse> :IPipelineBehavior<TRequest, TResponse>
     where TRequest : IBaseRequest,ITransactional
{
    public async  Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // Continue to next handler in the pipeline
                var response = await next();


                scope.Complete();
                return response;
            }
            catch (Exception)
            {
                scope.Dispose();
                throw;
            };
        }
           
        }
 }

 

using Core.Exceptions;
using Core.Pipelines.Logger;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
            catch (Exception ex)
            {
                if (ex is RollBackException || ex is not INonLogException)
                {
                    scope.Dispose();
                    throw;
                }

                scope.Complete();
                throw;
            }
           
        }
 }

  
}

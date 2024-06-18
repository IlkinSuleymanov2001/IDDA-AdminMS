using Application.Common.Exceptions;
using Application.Common.Pipelines.Logger;
using Application.Repositories.Context;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Pipelines.Transaction;

public class TransactionBehavior<TRequest, TResponse> : IAsyncDisposable,IPipelineBehavior<TRequest, TResponse>
     where TRequest : IBaseRequest,ITransactional
{
    private readonly AdminContext _dbContext;
    private bool _disposed = false;
    private  IDbContextTransaction _transaction;

    public TransactionBehavior(AdminContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async  Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        using (_transaction = await _dbContext.Database.BeginTransactionAsync())
        {

            try
            {
                // Continue to next handler in the pipeline
                var response = await next();

                await Done();
                return response;
            }
            catch (Exception ex)
            {
                if (ex is RollBackException || ex is not IException)
                {
                    await _transaction.RollbackAsync();
                    throw;
                }

                await Done();
                throw;
            }
 
             

           
        }


 }

   public async ValueTask DisposeAsync()
    {
            await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources
               await _dbContext.DisposeAsync();
            }

            // Dispose unmanaged resources
            _disposed = true;
        }
    }

    private async Task Done() 
    {
        await _dbContext.SaveChangesAsync();
        await _transaction.CommitAsync();
    }
}

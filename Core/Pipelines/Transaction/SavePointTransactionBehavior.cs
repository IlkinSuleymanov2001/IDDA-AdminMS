using Core.Exceptions;
using Core.Pipelines.Logger;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
namespace Core.Pipelines.Transaction;


public class SavePointTransactionBehavior<TRequest, TResponse, TContext> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IBaseRequest, ITransactionalSavePoint
     where TContext : DbContext
{

    private readonly TContext _dbContext;
    private IDbContextTransaction _transaction;

    public SavePointTransactionBehavior(TContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        using (_transaction = await _dbContext.Database.BeginTransactionAsync())
        {

            try
            {
                var response = await next();

                await CommitAsync(cancellationToken);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is RollBackException || ex is not IException)
                {
                    await RollBackSavePointOperationAsync(cancellationToken);
                    throw;
                }

                await CommitAsync(cancellationToken);
                throw;
            }




        }


    }

    private async Task CommitAsync(CancellationToken cancellationToken=default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
        await _transaction.CommitAsync(cancellationToken);


    }

    private async Task RollBackSavePointOperationAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.RollbackToSavepointAsync(SavePointNames.savepointone);
        await _transaction.CommitAsync(cancellationToken);
    }
}


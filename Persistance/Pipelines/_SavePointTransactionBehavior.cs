using Application.Repositories.Context;
using Core.Pipelines.Transaction;
using MediatR;

namespace Infastructure.Pipelines
{
    public class _SavePointTransactionBehavior<TRequest, TResponse> : SavePointTransactionBehavior<TRequest, TResponse, AdminContext>
         where TRequest : IBaseRequest, ITransactionalSavePoint
    {
        public _SavePointTransactionBehavior(AdminContext dbContext) : base(dbContext)
        {
        }
    }
}

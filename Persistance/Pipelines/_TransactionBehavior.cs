using Application.Repositories.Context;
using Core.Pipelines.Transaction;
using MediatR;

namespace Infastructure.Pipelines
{
    public class _TransactionBehavior<TRequest, TResponse> : TransactionBehavior<TRequest, TResponse, AdminContext>
     where TRequest : IBaseRequest,ITransactional
    {
        public _TransactionBehavior(AdminContext dbContext) : base(dbContext)
        {
        }
    }
}

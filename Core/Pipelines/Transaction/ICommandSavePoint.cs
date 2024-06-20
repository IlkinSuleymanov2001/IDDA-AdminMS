using MediatR;

namespace Core.Pipelines.Transaction
{
    public  interface  ICommandSavePoint:IRequest,ITransactionalSavePoint
    {
    }

    public interface ICommandSavePoint<TResult> : IRequest<TResult>, ITransactionalSavePoint
    {
    }
}

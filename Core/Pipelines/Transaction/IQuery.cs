using MediatR;

namespace Application.Common.Pipelines.Transaction
{
    public interface IQuery<T> : IRequest<T>
    {
    }
    public  interface IQuery :IRequest
    {
    }
}

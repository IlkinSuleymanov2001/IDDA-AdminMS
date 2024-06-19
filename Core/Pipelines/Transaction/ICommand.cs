using MediatR;

namespace Core.Pipelines.Transaction
{
    public  interface ICommand<T>:IRequest<T>,ITransactional
    {}

    public interface ICommand : IRequest, ITransactional { }
}

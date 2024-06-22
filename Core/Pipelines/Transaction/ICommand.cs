using MediatR;

namespace Core.Pipelines.Transaction
{
    public  interface ICommand<T>:IRequest<T>
    {}

    public interface ICommand : IRequest { }
}

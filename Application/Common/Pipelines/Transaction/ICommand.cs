using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Pipelines.Transaction
{
    public  interface ICommand<T>:IRequest<T>,ITransactional
    {}

    public interface ICommand : IRequest, ITransactional { }
}

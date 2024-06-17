using Application.Common.Pipelines.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public  class TransactionalException:Exception
    {
        public object? RealErrorMessage { get; set; }
        public TransactionalException()
        {
            
        }

        public TransactionalException(object realErrorMessage, string? message = "unsuccessful operations") : base(message)
        {
            RealErrorMessage = realErrorMessage;    
        }
    }
}

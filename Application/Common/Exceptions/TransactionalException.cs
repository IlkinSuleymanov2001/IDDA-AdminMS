using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public  class TransactionalException:Exception
    {
        public TransactionalException()
        {
            
        }

        public TransactionalException(string? message) : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public  class UnAuthorizationException :Exception
    {
        public UnAuthorizationException()
        {
            
        }

        public UnAuthorizationException(string message):base(message)
        {
            
        }
    }
}

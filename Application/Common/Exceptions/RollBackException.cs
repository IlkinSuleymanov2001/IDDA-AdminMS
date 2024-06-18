using Application.Common.Pipelines.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public  class RollBackException:Exception,IException
    {
        public RollBackException()
        {     
        }
        public RollBackException(string message):base(message)
        {
        
        }
    }
}

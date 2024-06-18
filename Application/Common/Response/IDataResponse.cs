using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Response
{
    public  interface IDataResponse:IResponse
    {
        object Data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Response
{
    public class Response : IResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}

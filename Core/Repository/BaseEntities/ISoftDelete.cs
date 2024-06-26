using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.BaseEntities
{
    public  interface ISoftDelete
    {
        public bool? IsDelete { get; set; }
    }
}

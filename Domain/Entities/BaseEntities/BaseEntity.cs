using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.BaseEntities
{
    public class BaseEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
}

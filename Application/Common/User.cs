using Application.Common.Mappings;
using Domain.Entities.BaseEntities;

namespace Application.Controllers
{
    public class User:BaseEntity<int>
    {
        public string  Name { get; set; }
    }
}



using Domain.Entities.BaseEntities;

namespace Domain.Entities
{
    public  class Category:BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;

      
    }
}

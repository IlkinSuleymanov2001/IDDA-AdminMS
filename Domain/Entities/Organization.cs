
using Domain.Entities.BaseEntities;

namespace Domain.Entities
{
    public  class Organization:BaseEntity<int>
    {
        public string  Name { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }

        public Organization()
        {
            Staffs = new HashSet<Staff>();
        }
    }
}

using Core.BaseEntities;
using Core.Repository.BaseEntities;

namespace Domain.Entities;

public  class Organization:BaseEntity<int>,IEntity
{
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Staff> Staffs { get; set; }

    public Organization()
    {
        Staffs = new HashSet<Staff>();
    }

    public Organization(string name):this()
    {
        Name = name;
    }
}

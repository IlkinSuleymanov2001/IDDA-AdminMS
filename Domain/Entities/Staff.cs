using Core.BaseEntities;

namespace Domain.Entities
{
    public  class Staff:BaseEntity<int>, IEntity
    {
        public string? Fullname { get; set; }
        public string  Username  { get; set; }
        public int OrganizationID { get; set; }

        public virtual Organization Organization { get; set; }

        public Staff(string? fullname, string username, int organizationID)
        {
            Fullname = fullname;
            Username = username;
            OrganizationID = organizationID;
        }

        public Staff()
        {      
        }
    }
}

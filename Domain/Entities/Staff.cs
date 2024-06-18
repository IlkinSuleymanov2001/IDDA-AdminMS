using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.BaseEntities;

namespace Domain.Entities
{
    public  class Staff:BaseEntity<int>
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

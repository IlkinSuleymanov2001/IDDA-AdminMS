using Core.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Index(nameof(Username),IsUnique =true)]
    [Table(name:"staff")]
    public  class Staff:BaseEntity<int>, IEntity
    {
        [MaxLength(100)]
        [Column("fullname", TypeName = "varchar(100)")]
        public string? Fullname { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        [Column("username", TypeName = "varchar(50)")]
        public string Username { get; set; }

        
        [ForeignKey(nameof(Organization))]
        [Column("organization_id")]
        public int OrganizationID { get; set; }



        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("active")] public bool? Active { get; set; } = true;


        public virtual Organization? Organization { get; set; }

        public Staff(string? fullname, string username, int organizationID):this()
        {
            Fullname = fullname;
            Username = username;
            OrganizationID = organizationID;
        }

        public Staff()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}

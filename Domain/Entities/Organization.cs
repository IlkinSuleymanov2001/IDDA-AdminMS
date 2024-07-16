using Core.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities;

[Table(name:"organization")]
[Index(nameof(Name),IsUnique =true)]
public sealed class Organization:BaseEntity<int>,IEntity
{
    //Changing the TypeName to "nvarchar" as it's a string property
    [Column("name", TypeName = "varchar(100)")]
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } 

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("active")] 
    public bool? Active { get; set; } = true;

    public ICollection<Staff>? Staffs { get; set; }

    public Organization()
    {
        CreatedAt = DateTime.UtcNow;
        Staffs = new HashSet<Staff>();
    }

}

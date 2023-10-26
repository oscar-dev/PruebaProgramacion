using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoPrueba.Models
{
    public class Organization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrganizationId { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Slug { get; set; }
    }
}

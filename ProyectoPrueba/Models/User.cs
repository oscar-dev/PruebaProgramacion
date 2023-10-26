using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPrueba.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Password { get; set; }
        
        public int OrganizationId { get; set; }
    }
}

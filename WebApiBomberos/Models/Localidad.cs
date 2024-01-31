using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiBomberos.Models
{
    [Table("LOCALIDAD")]
    public class Localidad
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
    }
}

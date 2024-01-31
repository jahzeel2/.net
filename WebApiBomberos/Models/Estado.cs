using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiBomberos.Models
{
    [Table("ESTADO")]
    public class Estado
    {
        [Key]
        public int id { get; set; }
        [StringLength(25)]
        [Required]
        public string nombre { get; set; }
    }
}

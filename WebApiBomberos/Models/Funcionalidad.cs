using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiBomberos.Models
{
    [Table("FUNCIONALIDAD")]
    public class Funcionalidad
    {
        [Key]
        public int id { get; set; }
        public int codigo { get; set; }
        [StringLength(50)]
        public string funcion { get; set; }
        [Required]
        public Boolean activo { get; set; }
    }
}

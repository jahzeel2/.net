using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiBomberos.Models
{
    [Table("TIPOSERVICIO")]
    public class TipoServicio
    {
        [Key]
        public int id { get; set; }
        [StringLength(75)]
        [Required]
        public string descripcion { get; set; }
        [Required]
        public Boolean activo { get; set; }
    }
}

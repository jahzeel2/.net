using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiBomberos.Models
{
    [Table("CATEGORIA")]
    public class Categoria
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int codigo { get; set; }
        [StringLength(20)]
        public string descripcion { get; set; }
        [StringLength(2)]
        public string categoria { get; set; }
        [Required]
        public int unidadFiscal { get; set; }
        [Required]
        public Boolean activo { get; set; }
    }
}

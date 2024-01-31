using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiBomberos.Models
{
    [Table("MONTOUNIDADFISCAL")]
    public class MontoUnidadFiscal
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int categoria { get; set; }
        [Precision(precision: 9, scale: 2)]
        [Required]
        public decimal monto { get; set; }
        public DateTime? fechaBaja { get; set; }
        [Required]
        public Boolean activo { get; set; }

        [ForeignKey("categoria")]
        public virtual Categoria? CategoriaRelacion { get; set; }
    }
}

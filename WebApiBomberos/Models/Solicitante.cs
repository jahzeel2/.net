using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace WebApiBomberos.Models
{
    [Table("SOLICITANTE")]
    public class Solicitante
    {
        [Key]
        public int id { get; set; }
        [StringLength(13)]
        public string dniCuitCuil { get; set; }
        [StringLength(25)]
        public string apellido { get; set; }
        [StringLength(25)]
        public string nombre { get; set; }
        [StringLength(50)]
        public string calle { get; set; }
        public int? numero { get; set; }
        public int? piso { get; set; }
        public int? dpto { get; set; }
        public int localidad { get; set; }
        [StringLength(50)]
        public string email { get; set; }
        [StringLength(20)]
        public string? telFijo { get; set; }
        [StringLength(20)]
        public string? celular { get; set; }
        [Required]
        public Boolean activo { get; set; }


        [ForeignKey("localidad")]
        public virtual Localidad? LocalidadRelacion { get; set; } 
    }
}

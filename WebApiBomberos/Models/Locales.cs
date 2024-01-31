using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiBomberos.Models
{
    [Table("LOCALES")]
    public class Locales
    {
        [Key]
        public int id { get; set; }
        [StringLength(30)]
        [Required]
        public string calle { get; set; }
        public int? nro { get; set; }
        public int? piso { get; set; }
        public int? dpto { get; set; }
        [StringLength(8)]
        public string? mzPcUf { get; set; }
        [Required]
        public int localidad { get; set; }
        [Required]
        public int superficieLocal { get; set; }
        [Required]
        public int montoCategoria { get; set; }
        [Required]
        public int personaSol { get; set; }
        [Required]
        public DateTime fechaAlta { get; set; }
        [Required]
        public int usuarioAlta { get; set; }
        public DateTime? fechaModif { get; set; }
        public int? usuarioModif { get; set; }
        public DateTime? fechaBaja { get; set; }
        public int? usuarioBaja { get; set; }
        [Required]
        public int activo { get; set; }

        [ForeignKey("localidad")]
        public virtual Localidad? localLocalidadRelacion { get; set; }
        [ForeignKey("usuarioAlta")]
        public virtual UsuarioBombero? usuarioAltaRelacion { get; set; }
        [ForeignKey("usuarioModif")]
        public virtual UsuarioBombero? usuarioModifRelacion { get; set; }
        [ForeignKey("usuarioBaja")]
        public virtual UsuarioBombero? usuarioBajaRelacion { get; set; }
        
    }
}

using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiBomberos.Models
{
    [Table("HABILITACION")]
    public class Habilitacion
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int localSolicitante { get; set; }
        [Required]
        public DateTime fechaSolicitud { get; set; }
        public int? estado { get; set; }
        public DateTime? fechaHabilitacion { get; set; }
        public DateTime? fechaVto { get; set; }
        [StringLength(100)]
        public string? observaciones { get; set; }
        [Required]
        public Boolean activo { get; set; }

        [ForeignKey("localSolicitante")]
        public virtual Locales? LocalRelacion { get; set; }
        [ForeignKey("estado")]
        public virtual Estado? EstadoRelacion { get; set; }
    }
}

using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace WebApiBomberos.Models
{
	[Table("IMPRESION")]
	public class Impresion
	{
		[Key]
        public int id { get; set; }
        [Required]
        public int localSol { get; set; }
		[Required]
		public int nroComprobante { get; set; }
		[Required]
		public DateTime fechaConfeccion { get; set; }
		[Required]
		public int funcionalidad { get; set; }
        [Required]
        public int tipoServicio { get; set; }
		public DateTime fechaPago { get; set; }
		[Precision(precision: 9, scale: 2)]
        [Required]
        public Decimal importe { get; set; }
		[StringLength(75)]
		public string? observaciones { get; set; }
        public DateTime fechaAlta { get; set; }
        [Required]
        public int usuarioAlta { get; set; }
        public DateTime? fechaModif { get; set; }
        public int? usuarioModif { get; set; }
        public DateTime? fechaBaja { get; set; }
        public int? usuarioBaja { get; set; }
        [Required]
        public Boolean activo { get; set; }


        [ForeignKey("localSol")]
        public virtual Locales? LocalesRelacion { get; set; }
        [ForeignKey("funcionalidad")]
        public virtual Funcionalidad? FuncionalidadRelacion { get; set; }
        [ForeignKey("tipoServicio")]
        public virtual TipoServicio? TipoServicioRelacion { get; set; }
    }
}

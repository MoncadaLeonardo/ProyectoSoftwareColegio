using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL

{
    [Table("EstadoSolicitud")]
    public class EstadoSolicitud
    {
        [Key]
        public int IdEstadoSolicitud { get; set; }

        [Required]
        [MaxLength(30)]
        public string NombreEstado { get; set; }= string.Empty;

        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? CreadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(50)]
        public string? ModificadoPor { get; set; }
    }
}

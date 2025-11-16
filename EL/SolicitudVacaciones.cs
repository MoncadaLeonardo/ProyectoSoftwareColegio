using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("SolicitudVacaciones")]
    public class SolicitudVacacion
    {
        [Key]
        public int IdSolicitud { get; set; }

        [Required]
        public int IdEmpleado { get; set; }

        [Required]
        public DateTime FechaSolicitud { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        public int? IdSupervisor { get; set; }

        public DateTime? FechaRevision { get; set; }

        [Required]
        public int IdEstadoSolicitud { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? CreadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(50)]
        public string? ModificadoPor { get; set; }
    }
}

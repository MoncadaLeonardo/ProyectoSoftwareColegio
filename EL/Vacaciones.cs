using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Vacaciones")]
    public class Vacacion
    {
        [Key]
        public int IdVacacion { get; set; }

        [Required]
        public int IdEmpleado { get; set; }

        public decimal DiasDisponibles { get; set; }
        public decimal DiasTomados { get; set; }
        public decimal DiasRestantes { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? CreadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(50)]
        public string? ModificadoPor { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("EmpleadoHorarios")]
    public class EmpleadoHorario
    {
        [Key]
        public int IdEmpleadoHorario { get; set; }

        [Required]
        public int IdEmpleado { get; set; }

        [Required]
        public int IdHorario { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? CreadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(50)]
        public string? ModificadoPor { get; set; }
    }
}

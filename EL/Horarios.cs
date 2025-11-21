using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Horarios")]
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }

        [Required]
        [MaxLength(50)]
        public string NombreHorario { get; set; } = string.Empty;

        
        [Required]
        public TimeSpan HoraEntrada { get; set; } = new TimeSpan(7, 0, 0); // 07:00 por defecto

        [Required]
        public TimeSpan HoraSalida { get; set; } = new TimeSpan(15, 0, 0); // 15:00 por defecto

        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? CreadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(50)]
        public string? ModificadoPor { get; set; }
    }
}

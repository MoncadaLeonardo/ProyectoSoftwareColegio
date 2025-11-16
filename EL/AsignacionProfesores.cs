using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("AsignacionProfesores")]
    public class AsignacionProfesor
    {
        [Key]
        public int IdAsignacion { get; set; }

        [Required]
        public int IdEmpleado { get; set; }

        [Required]
        public int IdAula { get; set; }

        [Required]
        public int IdSeccion { get; set; }

        [Required]
        public DateTime FechaAsignacion { get; set; }

        public bool Activa { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [MaxLength(50)]
       
        public string? CreadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(50)]
        public string? ModificadoPor { get; set; }

        public int? Ano { get; set; }
    }
}


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Empleados")]
    public class Empleado
    {
        [Key]
        public int IdEmpleado { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Apellido { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        public string CedulaNum { get; set; } = string.Empty;

        [MaxLength(30)]
        public string? Email { get; set; }

        [MaxLength(30)]
        public string? Telefono { get; set; }

        [MaxLength(30)]
        public string? TelefonoEmergencia { get; set; }

        public int? IdSupervisor { get; set; }
        public int? IdHorario { get; set; }
        public int? IdDepartamento { get; set; }

        public DateTime? FechaIngreso { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? CreadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(50)]
        public string? ModificadoPor { get; set; }

        public virtual ICollection<EmpleadoRol> EmpleadoRoles { get; set; } = new List<EmpleadoRol>();
    }
}



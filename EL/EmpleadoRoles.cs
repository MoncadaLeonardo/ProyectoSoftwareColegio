// EL/EmpleadoRol.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("EmpleadoRoles")]
    public class EmpleadoRol
    {
        [Key, Column(Order = 0)]
        public int IdEmpleado { get; set; }

        [Key, Column(Order = 1)]
        public int IdRol { get; set; }

        public DateTime FechaAsignacion { get; set; } = DateTime.Now;

        // Navegación (opcional)
        [ForeignKey("IdEmpleado")]
        public Empleado? Empleado { get; set; }

        [ForeignKey("IdRol")]
        public Rol? Rol { get; set; }
    }
}
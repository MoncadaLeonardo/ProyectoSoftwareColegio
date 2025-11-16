using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        public int IdEmpleado { get; set; }

        [Required]
        [MaxLength(50)]
        public string UsuarioNombre { get; set; } = string.Empty;

        [Required]
        public byte[] Contrasena { get; set; } = Array.Empty<byte>();

        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? CreadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(50)]
        public string? ModificadoPor { get; set; }

        public int? IdRoles { get; set; }
    }
}

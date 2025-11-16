using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Aulas")]
    public class Aula
    {
        [Key]
        public int IdAula { get; set; }

        [Required]
        [MaxLength(30)]
        public string NombreAula { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? CreadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(50)]
        public string? ModificadoPor { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantesWeb.Models
{
    public class Carrera
    {
        [Key]
        public int IdCarrera { get; set; }

        [Required(ErrorMessage = "El nombre de la carrera es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
        public virtual ICollection<Nivel> Niveles { get; set; } = new List<Nivel>();
    }
}

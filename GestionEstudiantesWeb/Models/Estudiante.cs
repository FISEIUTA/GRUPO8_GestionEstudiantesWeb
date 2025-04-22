using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantesWeb.Models
{
    public class Estudiante : Persona
    {
        [Key]
        public int IdEstudiante { get; set; }
        [Required(ErrorMessage = "Seleccione una carrera.")]
        public int IdCarrera { get; set; }

        [ForeignKey("IdCarrera")]
        public virtual Carrera? oCarrera { get; set; }

        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}

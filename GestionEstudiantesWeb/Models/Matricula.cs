using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantesWeb.Models
{
    public class Matricula
    {
        [Key]
        public int IdMatricula { get; set; }

        [Required]
        public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required]
        public int IdEstudiante { get; set; }

        [Required]
        public int IdMateria { get; set; }

        [ForeignKey("IdEstudiante")]
        public virtual Estudiante? oEstudiante { get; set; }

        [ForeignKey("IdMateria")]
        public virtual Materia? oMateria { get; set; } 

        public virtual ICollection<Nota> Notas { get; set; } = new List<Nota>();
    }
}

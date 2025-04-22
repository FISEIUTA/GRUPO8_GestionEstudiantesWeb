using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantesWeb.Models
{
    public class Materia
    {
        [Key]
        public int IdMateria { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Required]
        public int IdNivel { get; set; }

        [Required]
        public int IdDocente { get; set; }

        [ForeignKey("IdDocente")]
        public virtual Docente? oDocente { get; set; }

        [ForeignKey("IdNivel")]
        public virtual Nivel? oNivel { get; set; }

        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantesWeb.Models
{
    public class Nivel
    {
        [Key]
        public int IdNivel { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Required]
        public int IdCarrera { get; set; }

        [ForeignKey("IdCarrera")]
        public virtual Carrera? oCarrera { get; set; } 

        public virtual ICollection<Materia> Materias { get; set; } = new List<Materia>();
    }
}

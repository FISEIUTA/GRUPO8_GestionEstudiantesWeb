using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantesWeb.Models
{
    public class Nota
    {
        [Key]
        public int IdNota { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio.")]
        [StringLength(20)]
        public string Tipo { get; set; } = null!;

        [Required(ErrorMessage = "La calificación es obligatoria")]
        [Range(0, 10, ErrorMessage = "La calificación debe estar entre 0 y 10.")]
        public decimal Calificacion { get; set; }

        [Required]
        public int IdMatricula { get; set; }

        [ForeignKey("IdMatricula")]
        public virtual Matricula? oMatricula { get; set; }
    }
}

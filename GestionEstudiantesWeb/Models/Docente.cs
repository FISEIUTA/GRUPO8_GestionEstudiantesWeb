using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantesWeb.Models
{
    public class Docente : Persona
    {
        [Key]
        public int IdDocente { get; set; }
        public virtual ICollection<Materia> Materias { get; set; } = new List<Materia>();
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}

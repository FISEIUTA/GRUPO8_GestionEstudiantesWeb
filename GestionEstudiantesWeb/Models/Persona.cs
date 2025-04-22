using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantesWeb.Models
{
    public abstract class Persona
    {
        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "La cédula debe tener 10 dígitos")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "La cédula debe contener solo números")]
        public string Cedula { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 20 caracteres")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "El nombre solo puede contener letras")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El apellido debe tener entre 3 y 20 caracteres")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "El apellido solo puede contener letras")]
        public string Apellido { get; set; } = null!;

        [EmailAddress]
        [Required]
        public string Correo { get; set; } = null!;
    }
}

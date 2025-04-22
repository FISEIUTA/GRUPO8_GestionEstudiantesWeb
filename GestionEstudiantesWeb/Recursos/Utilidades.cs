using Microsoft.EntityFrameworkCore;
using GestionEstudiantesWeb.Data;
using GestionEstudiantesWeb.Models;

namespace GestionEstudiantesWeb.Recursos
{
    public static class Utilidades
    {
        public static string GenerarCorreo(Persona p)
        {
            string inicialNombre = p.Nombre.Trim().Substring(0, 1).ToLower();
            string apellido = p.Apellido.Trim().ToLower();
            string ultimos4 = p.Cedula.Substring(p.Cedula.Length - 4);
            return $"{inicialNombre}{apellido}{ultimos4}@uta.edu.ec";
        }

        public static string NormalizarTexto(string texto)
        {
            var palabras = texto.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => char.ToUpper(p[0]) + p.Substring(1));

            return string.Join(" ", palabras);
        }

        public static bool VerificarCampos(Persona p)
        {
            p.Cedula = p.Cedula.Trim();
            p.Nombre = p.Nombre.Trim();
            p.Apellido = p.Apellido.Trim();
            

            if (p.Nombre.Length > 2 && p.Nombre.Length < 20 && p.Apellido.Length > 2 && p.Apellido.Length < 20 &&
                p.Cedula.Length == 10) return true;
            return false;

        }

        public static bool YaExisteCedula(string cedula, AppDbContext _context)
        {
            return _context.Estudiantes.Any(e => e.Cedula == cedula) || _context.Docentes.Any(d => d.Cedula == cedula);
        }

        public static bool YaExisteCedula(string cedula, AppDbContext context, int? ExcluirPersona = null)
        {
            return context.Estudiantes.Any(e => e.Cedula == cedula && (!ExcluirPersona.HasValue || e.IdEstudiante != ExcluirPersona.Value))
                   || context.Docentes.Any(d => d.Cedula == cedula && (!ExcluirPersona.HasValue || d.IdDocente != ExcluirPersona.Value));
        }

    }
}

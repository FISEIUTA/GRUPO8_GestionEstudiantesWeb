using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionEstudiantesWeb.Data;
using GestionEstudiantesWeb.Models;
using GestionEstudiantesWeb.Recursos;

namespace GestionEstudiantesWeb.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly AppDbContext _context;

        public EstudiantesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Estudiantes.Include(e => e.oCarrera);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .Include(e => e.oCarrera)
                .FirstOrDefaultAsync(m => m.IdEstudiante == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "Nombre");
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Nombre,Apellido,IdCarrera")] Estudiante estudiante)
        {
            ModelState.Remove("Correo");
            if (ModelState.IsValid)
            {
                if(Utilidades.YaExisteCedula(estudiante.Cedula, _context))
                {
                    ModelState.AddModelError("", "La cédula ya está registrada.");
                    ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "Nombre", estudiante.IdCarrera);
                    return View(estudiante);
                }

                if (Utilidades.VerificarCampos(estudiante))
                {
                    estudiante.Nombre = Utilidades.NormalizarTexto(estudiante.Nombre);
                    estudiante.Apellido = Utilidades.NormalizarTexto(estudiante.Apellido);
                    estudiante.Correo = Utilidades.GenerarCorreo(estudiante);
                    _context.Add(estudiante);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Verifica los campos Nombre, Apellido y Cédula.");
                    ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "Nombre", estudiante.IdCarrera);
                    return View(estudiante);
                }
            }

            // Si llega aquí, es porque hay error de validación. ¡Hay que volver a llenar el ViewBag!
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "Nombre", estudiante?.IdCarrera);
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "Nombre", estudiante.IdCarrera);
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstudiante,Cedula,Nombre,Apellido,Correo,IdCarrera")] Estudiante estudiante)
        {
            if (id != estudiante.IdEstudiante)
            {
                return NotFound();
            }

            ModelState.Remove("Correo");
            if (ModelState.IsValid)
            {
                if (Utilidades.YaExisteCedula(estudiante.Cedula, _context, id))
                {
                    ModelState.AddModelError("", "La cédula ya está registrada.");
                    ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "Nombre", estudiante.IdCarrera);
                    return View(estudiante);
                }
                try
                {
                    //Valida que los campos tengan datos limpios
                    if (Utilidades.VerificarCampos(estudiante))
                    {
                        estudiante.Nombre = Utilidades.NormalizarTexto(estudiante.Nombre);
                        estudiante.Apellido = Utilidades.NormalizarTexto(estudiante.Apellido);
                        estudiante.Correo = Utilidades.GenerarCorreo(estudiante);
                        _context.Update(estudiante);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Verifica los campos Nombre, Apellido y Cédula.");
                        ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "Nombre", estudiante.IdCarrera);
                        return View(estudiante);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.IdEstudiante))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "IdCarrera", "Nombre", estudiante.IdCarrera);
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .Include(e => e.oCarrera)
                .FirstOrDefaultAsync(m => m.IdEstudiante == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiantes.Any(e => e.IdEstudiante == id);
        }
    }
}

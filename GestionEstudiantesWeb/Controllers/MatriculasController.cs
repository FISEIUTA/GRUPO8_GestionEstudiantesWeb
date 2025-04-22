using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionEstudiantesWeb.Data;
using GestionEstudiantesWeb.Models;

namespace GestionEstudiantesWeb.Controllers
{
    public class MatriculasController : Controller
    {
        private readonly AppDbContext _context;

        public MatriculasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Matriculas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Matriculas.Include(m => m.oEstudiante).Include(m => m.oMateria);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Matriculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.oEstudiante)
                .Include(m => m.oMateria)
                .FirstOrDefaultAsync(m => m.IdMatricula == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // GET: Matriculas/Create
        public IActionResult Create()
        {
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes.ToList(), "IdEstudiante", "NombreCompleto");
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "Nombre");
            return View();
        }

        // POST: Matriculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMatricula,IdEstudiante,IdMateria")] Matricula matricula)
        {

            ModelState.Remove("Fecha");

            bool yaInscrito = await _context.Matriculas.AnyAsync(m => m.IdEstudiante == matricula.IdEstudiante && m.IdMateria == matricula.IdMateria);

            if (yaInscrito)
            {
                ModelState.AddModelError("", "El estudiante ya está inscrito en esta materia.");
            }

            if (ModelState.IsValid)
            {
                matricula.Fecha = DateOnly.FromDateTime(DateTime.Now);
                _context.Add(matricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes.ToList(), "IdEstudiante", "NombreCompleto", matricula.IdEstudiante);
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "Nombre", matricula.IdMateria);
            return View(matricula);
        }

        // GET: Matriculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes.ToList(), "IdEstudiante", "NombreCompleto", matricula.IdEstudiante);
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "Nombre", matricula.IdMateria);
            return View(matricula);
        }

        // POST: Matriculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMatricula,Fecha,IdEstudiante,IdMateria")] Matricula matricula)
        {
            if (id != matricula.IdMatricula)
            {
                return NotFound();
            }

            ModelState.Remove("Fecha");

            bool yaInscrito = await _context.Matriculas.AnyAsync(m => m.IdEstudiante == matricula.IdEstudiante
                    && m.IdMateria == matricula.IdMateria && m.IdMatricula != id);

            if (yaInscrito)
            {
                ModelState.AddModelError("", "El estudiante ya está inscrito en esta materia.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaExists(matricula.IdMatricula))
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
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes.ToList(), "IdEstudiante", "NombreCompleto", matricula.IdEstudiante);
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "Nombre", matricula.IdMateria);
            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.oEstudiante)
                .Include(m => m.oMateria)
                .FirstOrDefaultAsync(m => m.IdMatricula == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula != null)
            {
                _context.Matriculas.Remove(matricula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matriculas.Any(e => e.IdMatricula == id);
        }
    }
}

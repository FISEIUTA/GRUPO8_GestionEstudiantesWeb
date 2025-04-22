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
    public class NotasController : Controller
    {
        private readonly AppDbContext _context;

        public NotasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var matriculas = _context.Notas.Include(n => n.oMatricula).ThenInclude(m => m.oEstudiante) // Asegura que carga el estudiante
                                      .Include(n => n.oMatricula) .ThenInclude(m => m.oMateria) // Asegura que carga la materia
                                      .ToListAsync();

            return View(await matriculas);
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas.Include(n => n.oMatricula).ThenInclude(m => m.oEstudiante)
                                           .Include(n => n.oMatricula).ThenInclude(m => m.oMateria)
                                           .FirstOrDefaultAsync(m => m.IdNota == id);
            
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            var listaMatriculas = _context.Matriculas
                .Include(m => m.oEstudiante).Select(m => new
                {IdMatricula = m.IdMatricula, NombreEstudiante = m.oEstudiante.Nombre + " " + m.oEstudiante.Apellido+" - " + m.oMateria.Nombre}).ToList();

            ViewData["IdMatricula"] = new SelectList(listaMatriculas, "IdMatricula", "NombreEstudiante");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNota,Tipo,Calificacion,IdMatricula")] Nota nota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMatricula"] = new SelectList(_context.Matriculas, "IdMatricula", "IdMatricula", nota.IdMatricula);
            return View(nota);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }

            var listaMatriculas = _context.Matriculas
                .Include(m => m.oEstudiante).Select(m => new
                { IdMatricula = m.IdMatricula, NombreEstudiante = m.oEstudiante.Nombre + " " + m.oEstudiante.Apellido + " - " + m.oMateria.Nombre }).ToList();


            ViewData["IdMatricula"] = new SelectList(listaMatriculas, "IdMatricula", "NombreEstudiante", nota.IdMatricula);
            return View(nota);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNota,Tipo,Calificacion,IdMatricula")] Nota nota)
        {
            if (id != nota.IdNota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaExists(nota.IdNota))
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
            ViewData["IdMatricula"] = new SelectList(_context.Matriculas, "IdMatricula", "IdMatricula", nota.IdMatricula);
            return View(nota);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas.Include(n => n.oMatricula).ThenInclude(m => m.oEstudiante)
                                                       .Include(n => n.oMatricula).ThenInclude(m => m.oMateria)
                                                       .FirstOrDefaultAsync(m => m.IdNota == id);

            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.IdNota == id);
        }
    }
}

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
    public class MateriasController : Controller
    {
        private readonly AppDbContext _context;

        public MateriasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Materias
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Materias.Include(m => m.oDocente).Include(m => m.oNivel)
                .Include(m => m.oNivel.oCarrera);
            
            return View(await appDbContext.ToListAsync());
        }

        // GET: Materias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materias
                .Include(m => m.oDocente)
                .Include(m => m.oNivel)
                .FirstOrDefaultAsync(m => m.IdMateria == id);
            if (materia == null)
            {
                return NotFound();
            }

            return View(materia);
        }

        // GET: Materias/Create
        public IActionResult Create()
        {
            ViewData["IdDocente"] = new SelectList(_context.Docentes.ToList(), "IdDocente", "NombreCompleto");
            ViewData["IdNivel"] = new SelectList(_context.Niveles, "IdNivel", "Nombre");
            return View();
        }

        // POST: Materias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMateria,Nombre,IdNivel,IdDocente")] Materia materia)
        {
            if (await _context.Materias.AnyAsync(m => m.Nombre.ToLower() == materia.Nombre.ToLower()))
            {
                ModelState.AddModelError("Nombre", "Ya existe una materia con ese nombre.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(materia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDocente"] = new SelectList(_context.Docentes.ToList(), "IdDocente", "NombreCompleto", materia.IdDocente);
            ViewData["IdNivel"] = new SelectList(_context.Niveles, "IdNivel", "Nombre", materia.IdNivel);
            return View(materia);
        }

        // GET: Materias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }
            ViewData["IdDocente"] = new SelectList(_context.Docentes.ToList(), "IdDocente", "NombreCompleto", materia.IdDocente);
            ViewData["IdNivel"] = new SelectList(_context.Niveles, "IdNivel", "Nombre", materia.IdNivel);
            return View(materia);
        }

        // POST: Materias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMateria,Nombre,IdNivel,IdDocente")] Materia materia)
        {
            if (id != materia.IdMateria)
            {
                return NotFound();
            }

            if (await _context.Materias.AnyAsync(m => m.Nombre.ToLower() == materia.Nombre.ToLower() && m.IdMateria != materia.IdMateria))
            {
                ModelState.AddModelError("Nombre", "Ya existe una materia con ese nombre.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriaExists(materia.IdMateria))
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
            ViewData["IdDocente"] = new SelectList(_context.Docentes, "IdDocente", "Apellido", materia.IdDocente);
            ViewData["IdNivel"] = new SelectList(_context.Niveles, "IdNivel", "Nombre", materia.IdNivel);
            return View(materia);
        }

        // GET: Materias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materias
                .Include(m => m.oDocente)
                .Include(m => m.oNivel)
                .FirstOrDefaultAsync(m => m.IdMateria == id);
            if (materia == null)
            {
                return NotFound();
            }

            return View(materia);
        }

        // POST: Materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia != null)
            {
                _context.Materias.Remove(materia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriaExists(int id)
        {
            return _context.Materias.Any(e => e.IdMateria == id);
        }
    }
}

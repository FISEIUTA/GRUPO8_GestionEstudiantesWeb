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
    public class DocentesController : Controller
    {
        private readonly AppDbContext _context;

        public DocentesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Docentes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Docentes.ToListAsync());
        }

        // GET: Docentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docente = await _context.Docentes
                .FirstOrDefaultAsync(m => m.IdDocente == id);
            if (docente == null)
            {
                return NotFound();
            }

            return View(docente);
        }

        // GET: Docentes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Docentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDocente,Cedula,Nombre,Apellido")] Docente docente)
        {
            ModelState.Remove("Correo");
            if (ModelState.IsValid)
            {
                if(Utilidades.YaExisteCedula(docente.Cedula, _context))
                {
                    ModelState.AddModelError("", "La cédula ya está registrada.");
                    return View(docente);
                }
                if (Utilidades.VerificarCampos(docente))
                {
                    docente.Nombre = Utilidades.NormalizarTexto(docente.Nombre);
                    docente.Apellido = Utilidades.NormalizarTexto(docente.Apellido);
                    docente.Correo = Utilidades.GenerarCorreo(docente);
                    _context.Add(docente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Verifica los campos Nombre, Apellido y Cédula.");
                    return View(docente);
                }
                
            }
            return View(docente);
        }

        // GET: Docentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docente = await _context.Docentes.FindAsync(id);
            if (docente == null)
            {
                return NotFound();
            }
            return View(docente);
        }

        // POST: Docentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDocente,Cedula,Nombre,Apellido,Correo")] Docente docente)
        {
            if (id != docente.IdDocente)
            {
                return NotFound();
            }

            ModelState.Remove("Correo");
            if (ModelState.IsValid)
            {
                if(Utilidades.YaExisteCedula(docente.Cedula, _context, id))
                {
                    ModelState.AddModelError("", "La cédula ya está registrada.");
                    return View(docente);
                }
                try
                {
                    if (Utilidades.VerificarCampos(docente))
                    {
                        docente.Nombre = Utilidades.NormalizarTexto(docente.Nombre);
                        docente.Apellido = Utilidades.NormalizarTexto(docente.Apellido);
                        docente.Correo = Utilidades.GenerarCorreo(docente);
                        _context.Update(docente);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Verifica los campos Nombre, Apellido y Cédula.");
                        return View(docente);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocenteExists(docente.IdDocente))
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
            return View(docente);
        }

        // GET: Docentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docente = await _context.Docentes
                .FirstOrDefaultAsync(m => m.IdDocente == id);
            if (docente == null)
            {
                return NotFound();
            }

            return View(docente);
        }

        // POST: Docentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);
            if (docente != null)
            {
                _context.Docentes.Remove(docente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocenteExists(int id)
        {
            return _context.Docentes.Any(e => e.IdDocente == id);
        }
    }
}

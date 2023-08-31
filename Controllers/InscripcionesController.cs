using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial01BM101219.Models;

namespace Parcial01BM101219.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly Bm101219Context _context;

        public InscripcionesController(Bm101219Context context)
        {
            _context = context;
        }

        // GET: Inscripciones
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                var bm101219Context = _context.Inscripciones.Include(i => i.Curso).Include(i => i.Estudiante);
                return View(await bm101219Context.ToListAsync());
            }
            else
            {
                return View("~/Views/Login/Login.cshtml");
            }
            
        }

        // GET: Inscripciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inscripciones == null)
            {
                return NotFound();
            }

            var inscripcione = await _context.Inscripciones
                .Include(i => i.Curso)
                .Include(i => i.Estudiante)
                .FirstOrDefaultAsync(m => m.InscripcionId == id);
            if (inscripcione == null)
            {
                return NotFound();
            }

            return View(inscripcione);
        }

        // GET: Inscripciones/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId");
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "EstudianteId", "EstudianteId");
            return View();
        }

        // POST: Inscripciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InscripcionId,EstudianteId,CursoId")] Inscripcione inscripcione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscripcione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", inscripcione.CursoId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "EstudianteId", "EstudianteId", inscripcione.EstudianteId);
            return View(inscripcione);
        }

        // GET: Inscripciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inscripciones == null)
            {
                return NotFound();
            }

            var inscripcione = await _context.Inscripciones.FindAsync(id);
            if (inscripcione == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", inscripcione.CursoId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "EstudianteId", "EstudianteId", inscripcione.EstudianteId);
            return View(inscripcione);
        }

        // POST: Inscripciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InscripcionId,EstudianteId,CursoId")] Inscripcione inscripcione)
        {
            if (id != inscripcione.InscripcionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscripcione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscripcioneExists(inscripcione.InscripcionId))
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
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", inscripcione.CursoId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "EstudianteId", "EstudianteId", inscripcione.EstudianteId);
            return View(inscripcione);
        }

        // GET: Inscripciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inscripciones == null)
            {
                return NotFound();
            }

            var inscripcione = await _context.Inscripciones
                .Include(i => i.Curso)
                .Include(i => i.Estudiante)
                .FirstOrDefaultAsync(m => m.InscripcionId == id);
            if (inscripcione == null)
            {
                return NotFound();
            }

            return View(inscripcione);
        }

        // POST: Inscripciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inscripciones == null)
            {
                return Problem("Entity set 'Bm101219Context.Inscripciones'  is null.");
            }
            var inscripcione = await _context.Inscripciones.FindAsync(id);
            if (inscripcione != null)
            {
                _context.Inscripciones.Remove(inscripcione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscripcioneExists(int id)
        {
          return (_context.Inscripciones?.Any(e => e.InscripcionId == id)).GetValueOrDefault();
        }
    }
}

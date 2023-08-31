﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial01BM101219.Models;

namespace Parcial01BM101219.Controllers
{
    public class DocentesController : Controller
    {
        private readonly Bm101219Context _context;

        public DocentesController(Bm101219Context context)
        {
            _context = context;
        }

        // GET: Docentes
        public async Task<IActionResult> Index()
        {

            if (1>0)
            {
                return _context.Docentes != null ?
                            View(await _context.Docentes.ToListAsync()) :
                            Problem("Entity set 'Bm101219Context.Docentes'  is null.");
            }
            else
            {
                return View("~/Views/Login/Login.cshtml");
            }
        }

        // GET: Docentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Docentes == null)
            {
                return NotFound();
            }

            var docente = await _context.Docentes
                .FirstOrDefaultAsync(m => m.DocenteId == id);
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
        public async Task<IActionResult> Create([Bind("DocenteId,Nombre")] Docente docente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(docente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(docente);
        }

        // GET: Docentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Docentes == null)
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
        public async Task<IActionResult> Edit(int id, [Bind("DocenteId,Nombre")] Docente docente)
        {
            if (id != docente.DocenteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(docente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocenteExists(docente.DocenteId))
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
            if (id == null || _context.Docentes == null)
            {
                return NotFound();
            }

            var docente = await _context.Docentes
                .FirstOrDefaultAsync(m => m.DocenteId == id);
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
            if (_context.Docentes == null)
            {
                return Problem("Entity set 'Bm101219Context.Docentes'  is null.");
            }
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
          return (_context.Docentes?.Any(e => e.DocenteId == id)).GetValueOrDefault();
        }
    }
}

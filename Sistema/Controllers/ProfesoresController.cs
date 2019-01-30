using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class ProfesoresController : Controller
    {
        private readonly Sistema_EscolarContext _context;

        public ProfesoresController(Sistema_EscolarContext context)
        {
            _context = context;
        }

        // GET: Profesores
        public async Task<IActionResult> Index()
        {
            var sistema_EscolarContext = _context.Profesores.Include(p => p.MateriaNavigation);
            return View(await sistema_EscolarContext.ToListAsync());
        }

        // GET: Profesores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesores = await _context.Profesores
                .Include(p => p.MateriaNavigation)
                .FirstOrDefaultAsync(m => m.ProfesorId == id);
            if (profesores == null)
            {
                return NotFound();
            }

            return View(profesores);
        }

        // GET: Profesores/Create
        public IActionResult Create()
        {
            ViewData["Materia"] = new SelectList(_context.Materia, "MateriaId", "NombreMateria");
            return View();
        }

        // POST: Profesores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfesorId,Nombre,Apellido,Edad,Correo,Materia,Pass")] Profesores profesores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Materia"] = new SelectList(_context.Materia, "MateriaId", "NombreMateria", profesores.Materia);
            return View(profesores);
        }

        // GET: Profesores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesores = await _context.Profesores.FindAsync(id);
            if (profesores == null)
            {
                return NotFound();
            }
            ViewData["Materia"] = new SelectList(_context.Materia, "MateriaId", "NombreMateria", profesores.Materia);
            return View(profesores);
        }

        // POST: Profesores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfesorId,Nombre,Apellido,Edad,Correo,Materia,Pass")] Profesores profesores)
        {
            if (id != profesores.ProfesorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesoresExists(profesores.ProfesorId))
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
            ViewData["Materia"] = new SelectList(_context.Materia, "MateriaId", "NombreMateria", profesores.Materia);
            return View(profesores);
        }

        // GET: Profesores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesores = await _context.Profesores
                .Include(p => p.MateriaNavigation)
                .FirstOrDefaultAsync(m => m.ProfesorId == id);
            if (profesores == null)
            {
                return NotFound();
            }

            return View(profesores);
        }

        // POST: Profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesores = await _context.Profesores.FindAsync(id);
            _context.Profesores.Remove(profesores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesoresExists(int id)
        {
            return _context.Profesores.Any(e => e.ProfesorId == id);
        }
    }
}

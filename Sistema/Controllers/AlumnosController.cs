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
    public class AlumnosController : Controller
    {
        private readonly Sistema_EscolarContext _context;


        public AlumnosController(Sistema_EscolarContext context)
        {
            _context = context;
            
        }

        // GET: Alumnos
        public async Task<IActionResult> Index()
        {
            ViewBag.materias = _context.Materia.ToList();
            var sistema_EscolarContext = _context.Alumnos.Include(a => a.MateriaNavigation).Include(a => a.ProfesorNavigation);
            return View(await sistema_EscolarContext.ToListAsync());
        }

        // GET: Alumnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumnos = await _context.Alumnos
                .Include(a => a.MateriaNavigation)
                .Include(a => a.ProfesorNavigation)
                .FirstOrDefaultAsync(m => m.AlumnoId == id);
            if (alumnos == null)
            {
                return NotFound();
            }

            return View(alumnos);
        }

        // GET: Alumnos/Create
        public IActionResult Create()
        {

            ViewData["Materia"] = new SelectList(_context.Materia, "MateriaId", "NombreMateria");
            ViewData["Profesor"] = new SelectList(_context.Profesores, "ProfesorId", "Nombre");
            return View();
        }

        // POST: Alumnos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlumnoId,Nombre,Apellido,Edad,Materia,Profesor,Pass")] Alumnos alumnos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alumnos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
          
            ViewData["Materia"] = new SelectList(_context.Materia, "MateriaId", "NombreMateria");
            ViewData["Profesor"] = new SelectList(_context.Profesores, "ProfesorId", "Nombre", alumnos.Profesor);
            return View(alumnos);
        }

        // GET: Alumnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumnos = await _context.Alumnos.FindAsync(id);
            if (alumnos == null)
            {
                return NotFound();
            }
            ViewData["Materia"] = new SelectList(_context.Materia, "MateriaId", "NombreMateria");
            ViewData["Profesor"] = new SelectList(_context.Profesores, "ProfesorId", "Nombre", alumnos.Profesor);
            return View(alumnos);
        }

        // POST: Alumnos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlumnoId,Nombre,Apellido,Edad,Materia,Profesor,Pass")] Alumnos alumnos)
        {
            if (id != alumnos.AlumnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumnos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnosExists(alumnos.AlumnoId))
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
            ViewData["Materia"] = new SelectList(_context.Materia, "MateriaId", "NombreMateria", alumnos.Materia);
            ViewData["Profesor"] = new SelectList(_context.Profesores, "ProfesorId", "Nombre", alumnos.Profesor);
            return View(alumnos);
        }

        // GET: Alumnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumnos = await _context.Alumnos
                .Include(a => a.MateriaNavigation)
                .Include(a => a.ProfesorNavigation)
                .FirstOrDefaultAsync(m => m.AlumnoId == id);
            if (alumnos == null)
            {
                return NotFound();
            }

            return View(alumnos);
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumnos = await _context.Alumnos.FindAsync(id);
            _context.Alumnos.Remove(alumnos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnosExists(int id)
        {
            return _context.Alumnos.Any(e => e.AlumnoId == id);
        }
        public JsonResult GetProfesor(int materia)
        {
            List<Profesores> Profe = new List<Profesores>();
            Profe = _context.Profesores.Where(a => a.Materia == materia ).ToList();
            Profe.Insert(0, new Profesores { ProfesorId = 0, Nombre = "Selecione" });
            return Json(new SelectList(Profe, "ProfesorId", "Nombre"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAWBD_fase3.Models;

namespace LAWBD_fase3.Controllers
{
    public class BibliotecariosController : Controller
    {
        private readonly BibliotecaContext _context;

        public BibliotecariosController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Bibliotecarios
        public async Task<IActionResult> Index()
        {
            var bibliotecaContext = _context.Bibliotecarios.Include(b => b.Biblioteca);
            return View(await bibliotecaContext.ToListAsync());
        }

        // GET: Bibliotecarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bibliotecario = await _context.Bibliotecarios
                .Include(b => b.Biblioteca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bibliotecario == null)
            {
                return NotFound();
            }

            return View(bibliotecario);
        }

        // GET: Bibliotecarios/Create
        public IActionResult Create()
        {
            ViewData["BibliotecaId"] = new SelectList(_context.Bibliotecas, "Id", "Id");
            return View();
        }

        // POST: Bibliotecarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Telefone,Senha,BibliotecaId")] Bibliotecario bibliotecario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bibliotecario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BibliotecaId"] = new SelectList(_context.Bibliotecas, "Id", "Id", bibliotecario.BibliotecaId);
            return View(bibliotecario);
        }

        // GET: Bibliotecarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bibliotecario = await _context.Bibliotecarios.FindAsync(id);
            if (bibliotecario == null)
            {
                return NotFound();
            }
            ViewData["BibliotecaId"] = new SelectList(_context.Bibliotecas, "Id", "Id", bibliotecario.BibliotecaId);
            return View(bibliotecario);
        }

        // POST: Bibliotecarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Telefone,Senha,BibliotecaId")] Bibliotecario bibliotecario)
        {
            if (id != bibliotecario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bibliotecario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BibliotecarioExists(bibliotecario.Id))
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
            ViewData["BibliotecaId"] = new SelectList(_context.Bibliotecas, "Id", "Id", bibliotecario.BibliotecaId);
            return View(bibliotecario);
        }

        // GET: Bibliotecarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bibliotecario = await _context.Bibliotecarios
                .Include(b => b.Biblioteca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bibliotecario == null)
            {
                return NotFound();
            }

            return View(bibliotecario);
        }

        // POST: Bibliotecarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bibliotecario = await _context.Bibliotecarios.FindAsync(id);
            if (bibliotecario != null)
            {
                _context.Bibliotecarios.Remove(bibliotecario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BibliotecarioExists(int id)
        {
            return _context.Bibliotecarios.Any(e => e.Id == id);
        }
    }
}

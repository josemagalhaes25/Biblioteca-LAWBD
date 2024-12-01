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
    public class EmprestimosController : Controller
    {
        private readonly BibliotecaContext _context;

        public EmprestimosController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Emprestimos
        public async Task<IActionResult> Index()
        {
            var bibliotecaContext = _context.Emprestimos.Include(e => e.Leitor).Include(e => e.Livro);
            return View(await bibliotecaContext.ToListAsync());
        }

        // GET: Emprestimos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimos
                .Include(e => e.Leitor)
                .Include(e => e.Livro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // GET: Emprestimos/Create
        public IActionResult Create()
        {
            ViewData["LeitorId"] = new SelectList(_context.Leitores, "Id", "Id");
            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Id");
            return View();
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LeitorId,LivroId,DataEmprestimo,DataDevolucao")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emprestimo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeitorId"] = new SelectList(_context.Leitores, "Id", "Id", emprestimo.LeitorId);
            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Id", emprestimo.LivroId);
            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }
            ViewData["LeitorId"] = new SelectList(_context.Leitores, "Id", "Id", emprestimo.LeitorId);
            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Id", emprestimo.LivroId);
            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LeitorId,LivroId,DataEmprestimo,DataDevolucao")] Emprestimo emprestimo)
        {
            if (id != emprestimo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprestimo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmprestimoExists(emprestimo.Id))
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
            ViewData["LeitorId"] = new SelectList(_context.Leitores, "Id", "Id", emprestimo.LeitorId);
            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Id", emprestimo.LivroId);
            return View(emprestimo);
        }

        // GET: Emprestimos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimos
                .Include(e => e.Leitor)
                .Include(e => e.Livro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo != null)
            {
                _context.Emprestimos.Remove(emprestimo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmprestimoExists(int id)
        {
            return _context.Emprestimos.Any(e => e.Id == id);
        }
    }
}

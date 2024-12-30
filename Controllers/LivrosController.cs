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
    public class LivrosController : Controller
    {
        private readonly BibliotecaContext _context;

        public LivrosController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Livros
        public async Task<IActionResult> Index(string searchString)
        {
            // Carregar livros, incluindo biblioteca e categoria
            var livros = from l in _context.Livros
                         .Include(l => l.Biblioteca)
                         .Include(l => l.Categoria)
                         select l;

            // Se searchString não for nulo ou vazio, filtra os livros pelo título ou autor
            if (!String.IsNullOrEmpty(searchString))
            {
                livros = livros.Where(l => l.Titulo.Contains(searchString) || l.Autor.Contains(searchString));
            }

            return View(await livros.ToListAsync());
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .Include(l => l.Biblioteca)
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            ViewData["BibliotecaId"] = new SelectList(_context.Bibliotecas, "Id", "Nome");
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            return View();
        }

        // POST: Livros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Autor,Ano,Disponivel,CategoriaId,BibliotecaId")] Livro livro, IFormFile imagem)
        {
            if (ModelState.IsValid)
            {
                // Se a imagem não for nula e tiver mais de 0 bytes, converte a imagem para um array de bytes
                if (imagem != null && imagem.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await imagem.CopyToAsync(stream);
                        livro.Imagem = stream.ToArray();
                    }
                }

                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BibliotecaId"] = new SelectList(_context.Bibliotecas, "Id", "Nome", livro.BibliotecaId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", livro.CategoriaId);
            return View(livro);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            ViewData["BibliotecaId"] = new SelectList(_context.Bibliotecas, "Id", "Nome", livro.BibliotecaId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", livro.CategoriaId);
            return View(livro);
        }

        // POST: Livros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Autor,Ano,Disponivel,CategoriaId,BibliotecaId")] Livro livr, IFormFile imagem)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imagem != null && imagem.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await imagem.CopyToAsync(ms);
                            livro.Imagem = ms.ToArray();
                        }
                    }
                    else
                    {
                        var existingLivro = await _context.Livros.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
                        livro.Imagem = existingLivro.Imagem;
                    }

                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
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
            ViewData["BibliotecaId"] = new SelectList(_context.Bibliotecas, "Id", "Nome", livro.BibliotecaId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", livro.CategoriaId);
            return View(livro);

            private bool LivroExists(int id)
            {
            return _context.Livros.Any(e => e.Id == id);
            }
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .Include(l => l.Biblioteca)
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro != null)
            {
                _context.Livros.Remove(livro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}
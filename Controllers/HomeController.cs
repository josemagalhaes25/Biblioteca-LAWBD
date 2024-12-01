using LAWBD_fase3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LAWBD_fase3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BibliotecaContext _context;

        // Construtor que recebe o logger e o contexto do banco de dados
        public HomeController(ILogger<HomeController> logger, BibliotecaContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Ação para a página inicial
        public async Task<IActionResult> Index()
        {
            // Criando uma instância do HomeViewModel
            var viewModel = new HomeViewModel
            {
                // Preenchendo o ViewModel com os livros do banco de dados
                Livros = await _context.Livros
                    .Include(l => l.Biblioteca)
                    .Include(l => l.Categoria)
                    .ToListAsync()
            };

            // Passando o ViewModel para a View
            return View(viewModel);
        }

        // Ação para a página de Privacidade
        public IActionResult Privacy()
        {
            return View();
        }

        // Ação para o erro
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
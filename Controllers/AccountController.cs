using Microsoft.AspNetCore.Mvc;
using LAWBD_fase3.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LAWBD_fase3.Controllers
{
    public class AccountController : Controller
    {
        private readonly BibliotecaContext _context;

        public AccountController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Account/SignUp
        public IActionResult SignUp()
        {
            // Verificar se o usuário está logado
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index", "Home"); // Redireciona para a página inicial
            }

            return View();
        }

        // POST: Account/SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(string nome, string telefone, string email, string password, string confirmPassword)
        {
            // Verifica se as senhas coincidem
            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "As senhas não coincidem!";
                return View();
            }

            // Verifica se o email já existe
            var existingUser = await _context.Leitores.SingleOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "Email já está em uso. Tente outro.";
                return View();
            }

            // Se as senhas coincidem e o email é único, cria o novo usuário
            var newUser = new Leitor
            {
                Nome = nome,  // Atribui o nome
                Telefone = telefone, // Atribui o telefone
                Email = email,
                Password = password // Senha
            };

            // Adiciona o novo usuário ao banco de dados
            _context.Leitores.Add(newUser);
            await _context.SaveChangesAsync();

            // Usando TempData para exibir a mensagem de sucesso
            TempData["SuccessMessage"] = "Registado com sucesso! Faça login para acessar.";

            // Redireciona para a página de login após o registro
            return RedirectToAction("Login");
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            // Verificar se o usuário já está logado
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index", "Home"); // Redireciona para a página inicial
            }

            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string login, string password)
        {
            // Verifica se o login (email ou telefone) corresponde ao que está no banco de dados
            var user = _context.Leitores.SingleOrDefault(u =>
                (u.Email == login || u.Telefone == login) && u.Password == password);

            if (user != null)
            {
                // Armazenar o ID do usuário na sessão
                HttpContext.Session.SetString("UserId", user.Id.ToString());

                // Definir a mensagem de sucesso para exibir após o login
                TempData["SuccessMessage"] = "Login realizado com sucesso!";

                // Redireciona para a página inicial
                return RedirectToAction("Index", "Home"); // Redireciona para a página inicial após o login
            }

            ViewBag.ErrorMessage = "Credenciais inválidas!";
            return View();
        }

        // Logout: Limpar a sessão e redirecionar para a página inicial
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Limpa todos os dados da sessão
            return RedirectToAction("Index", "Home"); // Redireciona para a página inicial
        }
    }
}
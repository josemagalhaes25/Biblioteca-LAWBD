using Microsoft.AspNetCore.Mvc;
using LAWBD_fase3.Models;

namespace LAWBD_fase3.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View("Dashboard");
        }

        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public IActionResult Library()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public IActionResult MyBooks()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public IActionResult Notifications()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", "Home");
        }
    }
}
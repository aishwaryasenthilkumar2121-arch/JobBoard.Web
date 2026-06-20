using Microsoft.AspNetCore.Mvc;
using JobBoard.Web.Data;
using JobBoard.Web.Models;
using Microsoft.AspNetCore.Http;

namespace JobBoard.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Email == email && x.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "Employer")
                {
                    return RedirectToAction("Index", "Job");
                }
                else
                {
                    return RedirectToAction("CandidateDashboard", "Job");
                }
            }

            ViewBag.Message = "Invalid Email or Password";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
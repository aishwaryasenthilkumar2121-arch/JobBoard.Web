using Microsoft.AspNetCore.Mvc;
using JobBoard.Web.Data;

using JobBoard.Web.Models;

namespace JobBoard.Controllers
{
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var jobs = _context.Jobs.ToList();
            return View(jobs);
        }

        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Employer")
            {
                return RedirectToAction("CandidateDashboard");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Jobs.Add(job);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }
        public IActionResult CandidateDashboard()
        {
            var jobs = _context.Jobs.ToList();
            return View(jobs);
        }
    }
}
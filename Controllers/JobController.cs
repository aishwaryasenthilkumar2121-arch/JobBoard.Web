using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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

        // Employer Dashboard
        public IActionResult Index()
        {
            var jobs = _context.Jobs.ToList();
            return View(jobs);
        }

        // Create Job - Only Employer Can Access
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
            var role = HttpContext.Session.GetString("Role");

            if (role != "Employer")
            {
                return RedirectToAction("CandidateDashboard");
            }

            if (ModelState.IsValid)
            {
                _context.Jobs.Add(job);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(job);
        }

        // Candidate Dashboard
        public IActionResult CandidateDashboard()
        {
            var jobs = _context.Jobs.ToList();
            return View(jobs);
        }
    }
}
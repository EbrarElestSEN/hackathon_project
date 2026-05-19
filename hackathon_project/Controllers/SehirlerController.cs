using hackathon_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hackathon_project.Controllers
{
    public class SehirlerController : Controller
    {
        private readonly HavuzYemekDbContext _context;

        public SehirlerController(HavuzYemekDbContext context)
        {
            _context = context;
        }

        // GET: Sehirler
        public async Task<IActionResult> Index()
        {
            // Veritabanındaki tüm şehirleri çekiyoruz
            var sehirler = await _context.Sehirlers.ToListAsync();
            return View(sehirler);
        }
    }
}
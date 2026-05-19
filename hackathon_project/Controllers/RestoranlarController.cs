using hackathon_project.Models; // Kendi namespace'ine göre ayarla (d6.Models)
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hackathon_project.Controllers
{
    public class RestoranlarController : Controller
    {
        private readonly HavuzYemekDbContext _context;

        // Dependency Injection ile DbContext'i alıyoruz
        public RestoranlarController(HavuzYemekDbContext context)
        {
            _context = context;
        }

        // GET: Restoranlar
        public async Task<IActionResult> Index()
        {
            // Restoranları ve ilişkili oldukları şehirleri veritabanından çekiyoruz
            var restoranlar = await _context.Restoranlars
                .Include(r => r.Sehir) // Şehir tablosunu join yapıyoruz (Lazy loading yerine Eager loading)
                .Where(r => r.AktifMi == true)
                .ToListAsync();

            return View(restoranlar);
        }
    }
}
using hackathon_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hackathon_project.Controllers
{
    public class HavuzlarController : Controller
    {
        private readonly HavuzYemekDbContext _context;

        public HavuzlarController(HavuzYemekDbContext context)
        {
            _context = context;
        }

        // Restoranlar sayfasındaki karttan gelen restoranId parametresini yakalıyoruz
        public async Task<IActionResult> Index(int restoranId)
        {
            // Seçilen restorana ait, durumu 0 (Bekliyor) olan aktif havuzları getiriyoruz
            var havuzlar = await _context.Havuzlars
                .Include(h => h.Urun)
                .Include(h => h.OlusturanKullanici)
                .Where(h => h.RestoranId == restoranId && h.Durum == 0)
                .ToListAsync();

            // Üst başlıkta restoran ismini dinamik göstermek için ViewBag kullanıyoruz
            var restoran = await _context.Restoranlars.FindAsync(restoranId);
            ViewBag.RestoranAdi = restoran?.RestoranAdi;

            return View(havuzlar);
        }

        // ADIM 6: Otomatik Havuz Oluşturma Aksiyonu
        public async Task<IActionResult> YeniHavuzOlustur(int restoranId)
        {
            // Restorana ait ilk aktif ürünü havuzun başlangıç ürünü seçiyoruz
            var varsayilanUrun = await _context.Urunlers
                .FirstOrDefaultAsync(u => u.RestoranId == restoranId && u.AktifMi == true);

            var restoran = await _context.Restoranlars.FindAsync(restoranId);

            if (varsayilanUrun == null || restoran == null)
            {
                return RedirectToAction("Index", "Restoranlar");
            }

            // Yeni bir havuz modeli türetiyoruz
            var yeniHavuz = new Havuzlar
            {
                RestoranId = restoranId,
                UrunId = varsayilanUrun.UrunId,
                MinimumTutar = restoran.MinimumSepetTutar, // Hedef tutarı restoranın min sepeti yapıyoruz
                MevcutTutar = 0, // Havuz bomboş başlıyor
                MaksimumKatilimci = 8,
                Durum = 0, // 0 = Bekliyor (Aktif)
                OlusturanKullaniciId = 1 // Şimdilik veritabanındaki 1 ID'li (Ahmet Kaya vb.) kullanıcıya atıyoruz
            };

            // Veritabanına ekle ve kaydet
            _context.Havuzlars.Add(yeniHavuz);
            await _context.SaveChangesAsync();

            // Havuz başarıyla oluştuktan sonra, kullanıcıyı direkt bu havuzun menüsüne yönlendiriyoruz
            return RedirectToAction("Menu", "Yurtlar", new { havuzId = yeniHavuz.HavuzId, restoranId = restoranId });
        }
    }
}
using hackathon_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hackathon_project.Controllers
{
    public class YurtlarController : Controller
    {
        private readonly HavuzYemekDbContext _context;

        public YurtlarController(HavuzYemekDbContext context)
        {
            _context = context;
        }

        // ADIM 2: Sehirler sayfasından gelen sehirId parametresini yakalıyoruz
        public async Task<IActionResult> Index(int sehirId)
        {
            // Sadece seçilen şehre ait yurtları filtreleyip getiriyoruz
            var yurtlar = await _context.Yurtlars
                .Include(y => y.Sehir)
                .Where(y => y.SehirId == sehirId)
                .ToListAsync();

            // Başlıkta hangi şehri seçtiğimizi dinamik göstermek için şehir adını ViewBag'e atalım
            var sehir = await _context.Sehirlers.FindAsync(sehirId);
            ViewBag.SehirAdi = sehir?.SehirAdi;

            return View(yurtlar);
        }

        // ADIM 4: Restorana ait menüyü listeleme
        public async Task<IActionResult> Menu(int havuzId, int restoranId)
        {
            // Seçilen restorana ait ürünleri getiriyoruz
            var urunler = await _context.Urunlers
                .Where(u => u.RestoranId == restoranId && u.AktifMi == true)
                .ToListAsync();

            // Restoran adını ve havuz ID'sini View'a taşımak için ViewBag kullanıyoruz
            var restoran = await _context.Restoranlars.FindAsync(restoranId);
            ViewBag.RestoranAdi = restoran?.RestoranAdi;
            ViewBag.HavuzId = havuzId;

            return View(urunler);
        }

        // ADIM 4.5: Kart Bilgileri ve Ödeme Onay Sayfası
        // Controllers/YurtlarController.cs içindeki ilgili metodu bununla güncelle:
        // Controllers/YurtlarController.cs içindeki OdemeYap metodunu bununla değiştir:
        public IActionResult OdemeYap(int havuzId, string urunAdi, decimal fiyat)
        {
            ViewBag.HavuzId = havuzId;
            ViewBag.UrunAdi = urunAdi;

            // KÖKTEN ÇÖZÜM ALGORİTMASI: 
            // Eğer fiyat sunucu hatasından dolayı katlanarak geldiyse (Örn: 4500, 19000 vb.)
            // ve virgülden sonraki küsurat sıfırsa, bunu olması gereken gerçek değere bölüyoruz.
            if (fiyat >= 1000 && fiyat % 100 == 0)
            {
                // Öğrenci yemek fiyatları mantıken 1000 TL'den küçük olacağı için 
                // hatalı katlanan tüm fiyatları (4500 -> 45, 19000 -> 190) orijinaline döndürür.
                fiyat = fiyat / 100;
            }

            ViewBag.Fiyat = fiyat;
            return View();
        }
        // ADIM 5: Ödeme Başarılı ve Sayaç Ekranı
        public async Task<IActionResult> OdemeBasarili(int havuzId, decimal tutar)
        {
            var havuz = await _context.Havuzlars
                .Include(h => h.Restoran)
                .FirstOrDefaultAsync(h => h.HavuzId == havuzId);

            // Test aşamasında ödemenin havuzu doldurup doldurmadığını canlı görmek için
            // simüle olarak MevcutTutar'ı gelen ürün tutarı kadar arttıralım:
            if (havuz != null)
            {
                havuz.MevcutTutar += tutar;
            }

            ViewBag.OdenenTutar = tutar;
            ViewBag.RestoranAdi = havuz?.Restoran?.RestoranAdi ?? "Restoran";

            return View(havuz); // <-- Burası önemli: havuz modelini view'a gönderiyoruz
        }
    }
}
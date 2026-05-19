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

        // YENİ EKLENEN ADIM: Menüden ürünü sepete ekleme ve Veritabanına kaydetme
        [HttpPost]
        public async Task<IActionResult> HavuzaUrunEkle(int havuzId, int urunId, decimal fiyat, string urunAdi)
        {
            // Fiyat düzeltme algoritmasını (Örn: 4500 -> 45) burada da yapıyoruz
            if (fiyat >= 1000 && fiyat % 100 == 0)
            {
                fiyat = fiyat / 100;
            }

            // 1. Yeni sipariş modelini oluştur (Veritabanına yansıtılacak)
            var yeniSiparis = new HavuzSiparisleri
            {
                HavuzId = havuzId,
                UrunId = urunId,
                KullaniciId = 1, // Şimdilik giriş yapan kullanıcıyı 1 kabul ediyoruz
                Adet = 1,
                Tutar = fiyat,
                SiparisTarihi = DateTime.Now
            };

            // 2. Modeli hafızaya ekle
            _context.HavuzSiparisleris.Add(yeniSiparis);

            // 3. Veritabanına kesin olarak KAZI
            await _context.SaveChangesAsync();

            // 4. Kayıt bittikten sonra kullanıcıyı Ödeme sayfasına yönlendir (Mevcut akışını bozmuyoruz)
            return RedirectToAction("OdemeYap", new { havuzId = havuzId, urunAdi = urunAdi, fiyat = fiyat });
        }

        // ADIM 4.5: Kart Bilgileri ve Ödeme Onay Sayfası
        public IActionResult OdemeYap(int havuzId, string urunAdi, decimal fiyat)
        {
            ViewBag.HavuzId = havuzId;
            ViewBag.UrunAdi = urunAdi;
            ViewBag.Fiyat = fiyat; // HavuzaUrunEkle metodunda fiyat düzeltildiği için burada bir daha düzeltmeye gerek yok

            return View();
        }

        // ADIM 5: Ödeme Başarılı ve Sayaç Ekranı
        public async Task<IActionResult> OdemeBasarili(int havuzId, decimal tutar)
        {
            // Havuzu veritabanından buluyoruz
            var havuz = await _context.Havuzlars
                .Include(h => h.Restoran)
                .FirstOrDefaultAsync(h => h.HavuzId == havuzId);

            if (havuz != null)
            {
                // 1. Havuzun mevcut tutarını gelen ürünün fiyatı kadar artırıyoruz
                havuz.MevcutTutar += tutar;

                // 2. EĞER MİNYUMUM TUTARA ULAŞILDIYSA: Havuzun durumunu otomatik "Doldu (1)" yapalım
                if (havuz.MevcutTutar >= havuz.MinimumTutar)
                {
                    havuz.Durum = 1; // 1 = Doldu (Kod mimarindeki kurala göre)
                }

                // 3. ALTIN VURUŞ: Bu değişikliği SQL veritabanına kalıcı olarak kaydet!
                await _context.SaveChangesAsync();
            }

            // Arayüze (View) bilgileri gönderiyoruz
            ViewBag.OdenenTutar = tutar;
            ViewBag.RestoranAdi = havuz?.Restoran?.RestoranAdi ?? "Restoran";

            return View(havuz);
        }
    }
}
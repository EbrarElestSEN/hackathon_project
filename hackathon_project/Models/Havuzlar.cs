using System;
using System.Collections.Generic;

namespace hackathon_project.Models;

public partial class Havuzlar
{
    public int HavuzId { get; set; }

    public int RestoranId { get; set; }

    public int UrunId { get; set; }

    public int OlusturanKullaniciId { get; set; }

    public decimal MinimumTutar { get; set; }

    public decimal MevcutTutar { get; set; }

    public int MaksimumKatilimci { get; set; }

    public byte Durum { get; set; }

    public DateTime? SonKatilimTarihi { get; set; }

    public DateTime OlusturmaTarihi { get; set; }

    public virtual ICollection<HavuzKatilimcilari> HavuzKatilimcilaris { get; set; } = new List<HavuzKatilimcilari>();

    public virtual ICollection<HavuzSayaclari> HavuzSayaclaris { get; set; } = new List<HavuzSayaclari>();

    public virtual ICollection<HavuzSiparisleri> HavuzSiparisleris { get; set; } = new List<HavuzSiparisleri>();

    public virtual Kullanicilar OlusturanKullanici { get; set; } = null!;

    public virtual Restoranlar Restoran { get; set; } = null!;

    public virtual Urunler Urun { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace hackathon_project.Models;

public partial class Kullanicilar
{
    public int KullaniciId { get; set; }

    public string AdSoyad { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefon { get; set; }

    public string SifreHash { get; set; } = null!;

    public int? SehirId { get; set; }

    public int? YurtId { get; set; }

    public DateTime KayitTarihi { get; set; }

    public bool AktifMi { get; set; }

    public virtual ICollection<HavuzKatilimcilari> HavuzKatilimcilaris { get; set; } = new List<HavuzKatilimcilari>();

    public virtual ICollection<HavuzSiparisleri> HavuzSiparisleris { get; set; } = new List<HavuzSiparisleri>();

    public virtual ICollection<Havuzlar> Havuzlars { get; set; } = new List<Havuzlar>();

    public virtual Sehirler? Sehir { get; set; }

    public virtual Yurtlar? Yurt { get; set; }
}

using System;
using System.Collections.Generic;

namespace hackathon_project.Models;

public partial class Urunler
{
    public int UrunId { get; set; }

    public int RestoranId { get; set; }

    public int KategoriId { get; set; }

    public string UrunAdi { get; set; } = null!;

    public string? Aciklama { get; set; }

    public decimal Fiyat { get; set; }

    public bool AktifMi { get; set; }

    public virtual ICollection<HavuzSiparisleri> HavuzSiparisleris { get; set; } = new List<HavuzSiparisleri>();

    public virtual ICollection<Havuzlar> Havuzlars { get; set; } = new List<Havuzlar>();

    public virtual Kategoriler Kategori { get; set; } = null!;

    public virtual Restoranlar Restoran { get; set; } = null!;
}

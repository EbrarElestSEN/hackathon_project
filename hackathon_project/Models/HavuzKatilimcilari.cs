using System;
using System.Collections.Generic;

namespace hackathon_project.Models;

public partial class HavuzKatilimcilari
{
    public int KatilimId { get; set; }

    public int HavuzId { get; set; }

    public int KullaniciId { get; set; }

    public string AnonymousAlias { get; set; } = null!;

    public decimal ToplamTutar { get; set; }

    public byte OdemeDurumu { get; set; }

    public DateTime KatilimZamani { get; set; }

    public DateTime? GuncellemeTarihi { get; set; }

    public virtual Havuzlar Havuz { get; set; } = null!;

    public virtual Kullanicilar Kullanici { get; set; } = null!;
}

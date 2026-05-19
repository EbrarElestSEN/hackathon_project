using System;
using System.Collections.Generic;

namespace hackathon_project.Models;

public partial class HavuzSiparisleri
{
    public int SiparisId { get; set; }

    public int HavuzId { get; set; }

    public int KullaniciId { get; set; }

    public int UrunId { get; set; }

    public int Adet { get; set; }

    public decimal Tutar { get; set; }

    public DateTime SiparisTarihi { get; set; }

    public virtual Havuzlar Havuz { get; set; } = null!;

    public virtual Kullanicilar Kullanici { get; set; } = null!;

    public virtual Urunler Urun { get; set; } = null!;
}

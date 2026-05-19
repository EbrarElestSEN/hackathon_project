using System;
using System.Collections.Generic;

namespace hackathon_project.Models;

public partial class Restoranlar
{
    public int RestoranId { get; set; }

    public string RestoranAdi { get; set; } = null!;

    public int SehirId { get; set; }

    public decimal MinimumSepetTutar { get; set; }

    public int? OrtalamaTeslimat { get; set; }

    public bool AktifMi { get; set; }

    public virtual ICollection<Havuzlar> Havuzlars { get; set; } = new List<Havuzlar>();

    public virtual Sehirler Sehir { get; set; } = null!;

    public virtual ICollection<Urunler> Urunlers { get; set; } = new List<Urunler>();
}

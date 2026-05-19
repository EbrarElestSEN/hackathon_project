using System;
using System.Collections.Generic;

namespace hackathon_project.Models;

public partial class Kategoriler
{
    public int KategoriId { get; set; }

    public string KategoriAdi { get; set; } = null!;

    public string? IkonUrl { get; set; }

    public int SiraNo { get; set; }

    public bool AktifMi { get; set; }

    public virtual ICollection<Urunler> Urunlers { get; set; } = new List<Urunler>();
}

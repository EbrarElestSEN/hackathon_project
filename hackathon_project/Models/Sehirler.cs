using System;
using System.Collections.Generic;

namespace hackathon_project.Models;

public partial class Sehirler
{
    public int SehirId { get; set; }

    public string SehirAdi { get; set; } = null!;

    public virtual ICollection<Kullanicilar> Kullanicilars { get; set; } = new List<Kullanicilar>();

    public virtual ICollection<Restoranlar> Restoranlars { get; set; } = new List<Restoranlar>();

    public virtual ICollection<Yurtlar> Yurtlars { get; set; } = new List<Yurtlar>();
}

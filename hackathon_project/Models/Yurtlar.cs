using System;
using System.Collections.Generic;

namespace hackathon_project.Models;

public partial class Yurtlar
{
    public int YurtId { get; set; }

    public int SehirId { get; set; }

    public string YurtAdi { get; set; } = null!;

    public virtual ICollection<Kullanicilar> Kullanicilars { get; set; } = new List<Kullanicilar>();

    public virtual Sehirler Sehir { get; set; } = null!;
}

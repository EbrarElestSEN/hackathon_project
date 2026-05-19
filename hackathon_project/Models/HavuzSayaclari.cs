using System;
using System.Collections.Generic;

namespace hackathon_project.Models;

public partial class HavuzSayaclari
{
    public int SayacId { get; set; }

    public int HavuzId { get; set; }

    public DateTime BaslangicZamani { get; set; }

    public DateTime BitisZamani { get; set; }

    public bool AktifMi { get; set; }

    public virtual Havuzlar Havuz { get; set; } = null!;
}

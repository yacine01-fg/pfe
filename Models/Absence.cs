using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Absence
{
    public int Idabsence { get; set; }

    public int? Iddetail { get; set; }

    public bool? Justifie { get; set; }

    public string? Motif { get; set; }

    public virtual Detailrapport? IddetailNavigation { get; set; }
}

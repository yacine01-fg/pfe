using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Heuresup
{
    public int Idhs { get; set; }

    public int? Iddetail { get; set; }

    public double? Dureehs { get; set; }

    public virtual Detailrapport? IddetailNavigation { get; set; }
}

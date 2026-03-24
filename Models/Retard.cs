using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Retard
{
    public int Idretard { get; set; }

    public int? Iddetail { get; set; }

    public double? Dureer { get; set; }

    public virtual Detailrapport? IddetailNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Sortieanticipee
{
    public int Idsortie { get; set; }

    public int? Iddetail { get; set; }

    public double? Durees { get; set; }

    public virtual Detailrapport? IddetailNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Strategiedegroupe
{
    public int Idgroupe { get; set; }

    public string? Nomgroupe { get; set; }

    public TimeOnly? Seuilentree { get; set; }

    public TimeOnly? Seuilsortie { get; set; }

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();
}

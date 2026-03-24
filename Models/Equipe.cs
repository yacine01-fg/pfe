using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Equipe
{
    public int Idequipe { get; set; }

    public string? Nomequipe { get; set; }

    public int? Idplanning { get; set; }

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();

    public virtual Planning? IdplanningNavigation { get; set; }
}

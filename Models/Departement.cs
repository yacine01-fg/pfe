using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Departement
{
    public int Iddept { get; set; }

    public string? Nomdept { get; set; }

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();
}

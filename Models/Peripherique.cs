using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Peripherique
{
    public int Idperi { get; set; }

    public string? Nomperi { get; set; }

    public string? Adresseip { get; set; }

    public virtual ICollection<Pointage> Pointages { get; set; } = new List<Pointage>();
}

using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Conge
{
    public int Idconge { get; set; }

    public int? Idemploye { get; set; }

    public DateOnly? Datedebut { get; set; }

    public DateOnly? Datefin { get; set; }

    public string? Typeconge { get; set; }

    public virtual Employe? IdemployeNavigation { get; set; }
}

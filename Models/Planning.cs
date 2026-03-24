using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Planning
{
    public int Idplanning { get; set; }

    public string? Nomplanning { get; set; }

    public TimeOnly? Horairedebut { get; set; }

    public TimeOnly? Horairefin { get; set; }

    public virtual ICollection<Equipe> Equipes { get; set; } = new List<Equipe>();
}

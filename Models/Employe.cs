using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Employe
{
    public int Idemp { get; set; }

    public string? Nomemp { get; set; }

    public string? Prenomemp { get; set; }


    public int? Idequipe { get; set; }

    public int? Idgroupe { get; set; }

    public virtual ICollection<Conge> Conges { get; set; } = new List<Conge>();

    public virtual ICollection<Detailrapport> Detailrapports { get; set; } = new List<Detailrapport>();


    public virtual Equipe? IdequipeNavigation { get; set; }

    public virtual Strategiedegroupe? IdgroupeNavigation { get; set; }

    public virtual ICollection<Pointage> Pointages { get; set; } = new List<Pointage>();
}
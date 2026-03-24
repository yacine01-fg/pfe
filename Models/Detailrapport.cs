using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Detailrapport
{
    public int Iddetail { get; set; }

    public int? Idemploye { get; set; }

    public DateOnly? Datedetail { get; set; }

    public string? Typedetail { get; set; }

    public virtual Absence? Absence { get; set; }

    public virtual Heuresup? Heuresup { get; set; }

    public virtual Employe? IdemployeNavigation { get; set; }

    public virtual Retard? Retard { get; set; }

    public virtual Sortieanticipee? Sortieanticipee { get; set; }
}

using System;
using System.Collections.Generic;

namespace PFE.Models;

public partial class Pointage
{
    public int Idp { get; set; }

    public int? Idemploye { get; set; }

    public int? Idperi { get; set; }

    public DateTime? Datep { get; set; }

    public string? Type { get; set; }

    public virtual Employe? IdemployeNavigation { get; set; }

    public virtual Peripherique? IdperiNavigation { get; set; }
}

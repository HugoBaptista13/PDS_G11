using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Cpostal
{
    public int IdCpostal { get; set; }

    public string Localidade { get; set; } = null!;

    public virtual ICollection<Morada> Morada { get; set; } = new List<Morada>();
}

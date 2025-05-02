using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class CoberturaGarantia
{
    public int IdCoberturaGarantia { get; set; }

    public int IdManutencao { get; set; }

    public int IdGarantia { get; set; }

    public virtual Garantia IdGarantiaNavigation { get; set; } = null!;

    public virtual Manutencao IdManutencaoNavigation { get; set; } = null!;
}

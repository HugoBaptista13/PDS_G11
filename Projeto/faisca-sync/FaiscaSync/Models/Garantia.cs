using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Garantia
{
    public int IdGarantia { get; set; }

    public DateTime Datainicio { get; set; }

    public DateTime Datafim { get; set; }

    public int IdEstadoGarantia { get; set; }

    public virtual ICollection<CoberturaGarantia> CoberturaGarantia { get; set; } = new List<CoberturaGarantia>();

    public virtual EstadoGarantia IdEstadoGarantiaNavigation { get; set; } = null!;

    public virtual ICollection<PosVenda> PosVenda { get; set; } = new List<PosVenda>();
}

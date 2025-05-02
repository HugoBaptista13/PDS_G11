using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class PosVenda
{
    public int IdPosVenda { get; set; }

    public int IdVendas { get; set; }

    public int IdGarantia { get; set; }

    public virtual Garantia IdGarantiaNavigation { get; set; } = null!;

    public virtual Venda IdVendasNavigation { get; set; } = null!;

    public virtual ICollection<Manutencao> Manutencaos { get; set; } = new List<Manutencao>();
}

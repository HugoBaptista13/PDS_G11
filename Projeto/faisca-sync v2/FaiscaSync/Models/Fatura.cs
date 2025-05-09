using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Fatura
{
    public int IdFatura { get; set; }

    public DateTime Dataemissao { get; set; }

    public decimal Valorfatura { get; set; }

    public string TipoPagamento { get; set; } = null!;

    public int IdCliente { get; set; }

    public int? IdVendas { get; set; }

    public int? IdManutencao { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Manutencao? IdManutencaoNavigation { get; set; }

    public virtual Venda? IdVendasNavigation { get; set; }
}

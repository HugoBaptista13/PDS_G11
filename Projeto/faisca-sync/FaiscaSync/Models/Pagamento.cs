using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Pagamento
{
    public int IdPagamento { get; set; }

    public DateTime Datapagamento { get; set; }

    public decimal Valorpagamento { get; set; }

    public int IdFatura { get; set; }

    public int IdTipoPagamento { get; set; }

    public virtual Fatura IdFaturaNavigation { get; set; } = null!;

    public virtual TipoPagamento IdTipoPagamentoNavigation { get; set; } = null!;
}

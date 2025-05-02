using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class TipoPagamento
{
    public int IdTipoPagamento { get; set; }

    public string Descricaotipopagamento { get; set; } = null!;

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}

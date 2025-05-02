using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Fatura
{
    public int IdFatura { get; set; }

    public DateTime Dataemissao { get; set; }

    public decimal Valorfatura { get; set; }

    public int IdVendas { get; set; }

    public virtual Venda IdVendasNavigation { get; set; } = null!;

    public virtual ICollection<Manutencao> Manutencaos { get; set; } = new List<Manutencao>();

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}

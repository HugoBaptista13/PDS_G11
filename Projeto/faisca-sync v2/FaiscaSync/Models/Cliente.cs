using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nome { get; set; } = null!;

    public DateTime Datanasc { get; set; }

    public string Nif { get; set; } = null!;

    public string Contato { get; set; } = null!;

    public int IdMorada { get; set; }

    public virtual ICollection<Fatura> Faturas { get; set; } = new List<Fatura>();

    public virtual Morada IdMoradaNavigation { get; set; } = null!;

    public virtual ICollection<Venda> Venda { get; set; } = new List<Venda>();
}

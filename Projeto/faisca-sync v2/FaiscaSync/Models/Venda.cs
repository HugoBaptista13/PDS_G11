using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Venda
{
    public int IdVendas { get; set; }

    public DateTime Datavenda { get; set; }

    public decimal Valorvenda { get; set; }

    public int IdVeiculo { get; set; }

    public int IdCliente { get; set; }

    public int IdFuncionario { get; set; }

    public virtual ICollection<Fatura> Faturas { get; set; } = new List<Fatura>();

    public virtual ICollection<Garantia> Garantia { get; set; } = new List<Garantia>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Funcionario IdFuncionarioNavigation { get; set; } = null!;

    public virtual Veiculo IdVeiculoNavigation { get; set; } = null!;
}

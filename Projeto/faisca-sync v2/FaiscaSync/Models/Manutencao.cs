using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Manutencao
{
    public int IdManutencao { get; set; }

    public DateTime Datamanutencao { get; set; }

    public string Tipomanutencao { get; set; } = null!;

    public decimal Custo { get; set; }

    public string Descricaomanutencao { get; set; } = null!;

    public int IdVeiculo { get; set; }

    public int IdGarantia { get; set; }

    public int IdEstadoManutencao { get; set; }

    public int IdFuncionario { get; set; }

    public virtual ICollection<Fatura> Faturas { get; set; } = new List<Fatura>();

    public virtual EstadoManutencao IdEstadoManutencaoNavigation { get; set; } = null!;

    public virtual Funcionario IdFuncionarioNavigation { get; set; } = null!;

    public virtual Garantia IdGarantiaNavigation { get; set; } = null!;

    public virtual Veiculo IdVeiculoNavigation { get; set; } = null!;
}

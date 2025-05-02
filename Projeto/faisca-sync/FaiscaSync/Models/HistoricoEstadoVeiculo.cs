using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class HistoricoEstadoVeiculo
{
    public int IdHistEstadoVeiculo { get; set; }

    public DateTime Dataalteracao { get; set; }

    public int IdEstadoVeiculo { get; set; }

    public int IdVeiculo { get; set; }

    public int IdFuncionario { get; set; }

    public virtual EstadoVeiculo IdEstadoVeiculoNavigation { get; set; } = null!;

    public virtual Funcionario IdFuncionarioNavigation { get; set; } = null!;

    public virtual Veiculo IdVeiculoNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class EstadoVeiculo
{
    public int IdEstadoVeiculo { get; set; }

    public string Descricaoestadoveiculo { get; set; } = null!;

    public virtual ICollection<HistoricoEstadoVeiculo> HistoricoEstadoVeiculos { get; set; } = new List<HistoricoEstadoVeiculo>();

    public virtual ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}

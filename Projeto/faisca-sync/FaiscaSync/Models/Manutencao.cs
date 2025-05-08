using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Manutencao
{
    public int IdManutencao { get; set; }

    public DateTime Datamanutencao { get; set; }

    public string Tipomanutencao { get; set; } = null!;

    public decimal Custo { get; set; }

    public string Descricaomanutencao { get; set; } = string.Empty;

    public int IdVeiculo { get; set; }

    public int IdPosVenda { get; set; }

    public int IdEstadoManutencao { get; set; }
    public DateTime? DataAgendada { get; set; }
    public int FaturaIdFatura { get; set; }

    public virtual ICollection<CoberturaGarantia> CoberturaGarantia { get; set; } = new List<CoberturaGarantia>();

    public virtual Fatura FaturaIdFaturaNavigation { get; set; } = null!;

    public virtual EstadoManutencao IdEstadoManutencaoNavigation { get; set; } = null!;

    public virtual PosVenda IdPosVendaNavigation { get; set; } = null!;

    public virtual Veiculo IdVeiculoNavigation { get; set; } = null!;
}

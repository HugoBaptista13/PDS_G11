using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Veiculo
{
    public int IdVeiculo { get; set; }
    public string Matricula { get; set; } = null!;

    public string Chassi { get; set; } = null!;

    public int Anofabrico { get; set; }

    public string Cor { get; set; } = null!;

    public decimal Quilometros { get; set; }

    public decimal Preco { get; set; }

    public int IdMotor { get; set; }

    public int IdEstadoVeiculo { get; set; }


    public int IdTipoVeiculo { get; set; }

    public int IdModelo { get; set; }

    public int IdAquisicao { get; set; }

    public string? FotoUrl { get; set; }
    public string? DescricaoExtras { get; set; }
    public virtual Aquisicao Aquisicao { get; set; }


    public virtual ICollection<HistoricoEstadoVeiculo> HistoricoEstadoVeiculos { get; set; } = new List<HistoricoEstadoVeiculo>();

    public virtual Aquisicao IdAquisicaoNavigation { get; set; } = null!;

    public virtual EstadoVeiculo IdEstadoVeiculoNavigation { get; set; } = null!;


    public virtual Modelo IdModeloNavigation { get; set; } = null!;

    public virtual Motor IdMotorNavigation { get; set; } = null!;

    public virtual TipoVeiculo IdTipoVeiculoNavigation { get; set; } = null!;

    public virtual ICollection<Manutencao> Manutencaos { get; set; } = new List<Manutencao>();

    public virtual ICollection<Venda> Venda { get; set; } = new List<Venda>();

}

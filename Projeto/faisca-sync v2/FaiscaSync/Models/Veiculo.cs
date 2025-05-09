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

    public int Quilometros { get; set; }

    public decimal Precovenda { get; set; }

    public string Fornecedor { get; set; } = null!;

    public decimal Valorpago { get; set; }

    public DateTime Dataaquisicao { get; set; }

    public string Origemveiculo { get; set; } = null!;

    public string? Foto { get; set; }

    public string? Descricao { get; set; }

    public int IdMotor { get; set; }

    public int IdEstadoVeiculo { get; set; }

    public int IdTipoVeiculo { get; set; }

    public int IdModelo { get; set; }

    public int IdFuncionario { get; set; }

    public virtual EstadoVeiculo IdEstadoVeiculoNavigation { get; set; } = null!;

    public virtual Funcionario IdFuncionarioNavigation { get; set; } = null!;

    public virtual Modelo IdModeloNavigation { get; set; } = null!;

    public virtual Motor IdMotorNavigation { get; set; } = null!;

    public virtual TipoVeiculo IdTipoVeiculoNavigation { get; set; } = null!;

    public virtual ICollection<Manutencao> Manutencaos { get; set; } = new List<Manutencao>();

    public virtual ICollection<Venda> Venda { get; set; } = new List<Venda>();
}

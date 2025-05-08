using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Aquisicao
{
    public int IdAquisicao { get; set; }

    public string Fornecedor { get; set; } = null!;

    public int FuncionarioId { get; set; }
    public virtual Funcionario IdfuncionarioidNavegation { get; set; } = null!;

    public decimal Valorpago { get; set; }

    public DateTime Dataaquisicao { get; set; }

    public string Origemveiculo { get; set; } = null!;

    public bool AprovadoFinanceiro { get; set; }
    public bool AprovadoAdmin { get; set; }
    public bool Finalizada { get; set; }


    public virtual ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();


}

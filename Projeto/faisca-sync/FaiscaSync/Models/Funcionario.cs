using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Funcionario
{
    public int IdFuncionario { get; set; }

    public string Nome { get; set; } = null!;

    public DateTime Datacontratacao { get; set; }

    public DateTime Datanascimento { get; set; }

    public int IdCargo { get; set; }

    public virtual ICollection<Contato> Contatos { get; set; } = new List<Contato>();

    public virtual ICollection<HistoricoEstadoVeiculo> HistoricoEstadoVeiculos { get; set; } = new List<HistoricoEstadoVeiculo>();

    public virtual Cargo IdCargoNavigation { get; set; } = null!;

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    public virtual ICollection<Venda> Venda { get; set; } = new List<Venda>();
}

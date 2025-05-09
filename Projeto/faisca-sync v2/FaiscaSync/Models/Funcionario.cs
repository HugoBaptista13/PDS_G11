using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Funcionario
{
    public int IdFuncionario { get; set; }

    public string Nome { get; set; } = null!;

    public DateTime Datacontratacao { get; set; }

    public DateTime Datanascimento { get; set; }

    public string Contato { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
    public int IdCargo { get; set; }

    public virtual Cargo IdCargoNavigation { get; set; } = null!;

    public virtual ICollection<Manutencao> Manutencaos { get; set; } = new List<Manutencao>();

    public virtual ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();

    public virtual ICollection<Venda> Venda { get; set; } = new List<Venda>();
}

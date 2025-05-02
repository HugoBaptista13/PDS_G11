using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Contato
{
    public int Idcontato { get; set; }

    public string Detalhescontato { get; set; } = null!;

    public int IdCliente { get; set; }

    public int IdTipoContato { get; set; }

    public int IdFuncionario { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Funcionario IdFuncionarioNavigation { get; set; } = null!;

    public virtual TipoContato IdTipoContatoNavigation { get; set; } = null!;
}

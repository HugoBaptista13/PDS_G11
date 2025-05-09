using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Cargo
{
    public int IdCargo { get; set; }

    public string Nomecargo { get; set; } = null!;

    public virtual ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
}

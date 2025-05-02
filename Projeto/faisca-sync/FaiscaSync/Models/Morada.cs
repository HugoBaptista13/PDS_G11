using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Morada
{
    public int IdMorada { get; set; }

    public string Rua { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public string Descricaomorada { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public int IdCpostal { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual Cpostal IdCpostalNavigation { get; set; } = null!;
}

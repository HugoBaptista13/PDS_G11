using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class TipoVeiculo
{
    public int IdTipoVeiculo { get; set; }

    public string Descricaotveiculo { get; set; } = null!;

    public virtual ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}

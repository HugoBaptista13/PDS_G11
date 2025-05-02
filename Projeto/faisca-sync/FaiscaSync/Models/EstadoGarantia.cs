using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class EstadoGarantia
{
    public int IdEstadoGarantia { get; set; }

    public string Descricaoestadogarantia { get; set; } = null!;

    public virtual ICollection<Garantia> Garantia { get; set; } = new List<Garantia>();
}

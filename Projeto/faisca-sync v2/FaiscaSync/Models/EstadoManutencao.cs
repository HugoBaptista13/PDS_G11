using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class EstadoManutencao
{
    public int IdEstadoManutencao { get; set; }

    public string Descricaoestadomanutencao { get; set; } = null!;

    public virtual ICollection<Manutencao> Manutencaos { get; set; } = new List<Manutencao>();
}

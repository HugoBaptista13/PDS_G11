using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string Descricaomarca { get; set; } = null!;

    public virtual ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
}

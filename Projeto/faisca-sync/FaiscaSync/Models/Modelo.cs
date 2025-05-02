using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Modelo
{
    public int IdModelo { get; set; }

    public string Nomemodelo { get; set; } = null!;

    public int IdMarca { get; set; }

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}

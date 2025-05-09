using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Motor
{
    public int IdMotor { get; set; }

    public string Tipomotor { get; set; } = null!;

    public string Potencia { get; set; } = null!;

    public string Combustivel { get; set; } = null!;

    public virtual ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}

using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Garantia
{
    public int IdGarantia { get; set; }

    public DateTime Datainicio { get; set; }

    public DateTime Datafim { get; set; }

    public int IdVendas { get; set; }

    public int IdEstadoGarantia { get; set; }

    public virtual EstadoGarantia IdEstadoGarantiaNavigation { get; set; } = null!;

    public virtual Venda IdVendasNavigation { get; set; } = null!;

    public virtual ICollection<Manutencao> Manutencaos { get; set; } = new List<Manutencao>();
}

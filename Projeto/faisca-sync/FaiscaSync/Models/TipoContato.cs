using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class TipoContato
{
    public int IdTipoContato { get; set; }

    public string Descricaotipocontato { get; set; } = null!;

    public virtual ICollection<Contato> Contatos { get; set; } = new List<Contato>();
}

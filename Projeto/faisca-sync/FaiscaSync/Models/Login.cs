using System;
using System.Collections.Generic;

namespace FaiscaSync.Models;

public partial class Login
{
    public int IdLogin { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdFuncionario { get; set; }

    public virtual Funcionario IdFuncionarioNavigation { get; set; } = null!;
}

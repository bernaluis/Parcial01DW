using System;
using System.Collections.Generic;

namespace Parcial01BM101219.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}

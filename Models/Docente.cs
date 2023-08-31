using System;
using System.Collections.Generic;

namespace Parcial01BM101219.Models;

public partial class Docente
{
    public int DocenteId { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}

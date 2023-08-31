using System;
using System.Collections.Generic;

namespace Parcial01BM101219.Models;

public partial class Estudiante
{
    public int EstudianteId { get; set; }

    public string? Nombre { get; set; }

    public int UsuarioId { get; set; }

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    public virtual Usuario Usuario { get; set; } = null!;
}

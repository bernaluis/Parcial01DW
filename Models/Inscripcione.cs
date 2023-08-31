using System;
using System.Collections.Generic;

namespace Parcial01BM101219.Models;

public partial class Inscripcione
{
    public int InscripcionId { get; set; }

    public int EstudianteId { get; set; }

    public int CursoId { get; set; }

    public virtual Curso Curso { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}

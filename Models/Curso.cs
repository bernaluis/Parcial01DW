using System;
using System.Collections.Generic;

namespace Parcial01BM101219.Models;

public partial class Curso
{
    public int CursoId { get; set; }

    public string? NombreCurso { get; set; }

    public int DocenteId { get; set; }

    public virtual Docente Docente { get; set; } = null!;

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();
}

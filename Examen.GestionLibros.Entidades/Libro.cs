using System;
using System.Collections.Generic;

namespace Examen.GestionLibros.Entidades;

public partial class Libro:EntidadBase
{

    public string Titulo { get; set; } = null!;

    public int TipoLibroId { get; set; }

    public int AutorId { get; set; }

    public DateOnly? FechaPublicacion { get; set; }

    public string? Isbn { get; set; }

    public int? Stock { get; set; }


    public virtual Autor Autor { get; set; } = null!;

    public virtual TipoLibro TipoLibro { get; set; } = null!;
}

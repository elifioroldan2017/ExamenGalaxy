using System;
using System.Collections.Generic;

namespace Examen.GestionLibros.Entidades;

public partial class TipoLibro:EntidadBase
{

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }


    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}

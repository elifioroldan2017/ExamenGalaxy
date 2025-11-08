using System;
using System.Collections.Generic;

namespace Examen.GestionLibros.Entidades;

public partial class Autor:EntidadBase
{

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Nacionalidad { get; set; }


    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}

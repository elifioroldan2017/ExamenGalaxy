using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.DTO.Request
{
   public class LibroRequest
    {
        public string Titulo { get; set; } = null!;

        public int TipoLibroId { get; set; }

        public int AutorId { get; set; }

        public DateOnly? FechaPublicacion { get; set; }

        public string? Isbn { get; set; }

        public int? Stock { get; set; }
    }
}

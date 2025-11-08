using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.DTO.Response
{
   public class LibroResponse
    {

        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string TipoLibro { get; set; }

        public string Autor { get; set; }

        public DateOnly? FechaPublicacion { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.DTO.Request
{
   public class AutorRequest
    {
        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string? Nacionalidad { get; set; }
    }
}

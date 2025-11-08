using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.Comun.Excepciones
{
   public class ExcepcionSistema: Exception
    {
        public int ErrorCode { get; }

        public ExcepcionSistema() { }

        public ExcepcionSistema(string message) : base(message) { }

        public ExcepcionSistema(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ExcepcionSistema(string message, Exception innerException) : base(message, innerException) { }
    }
}

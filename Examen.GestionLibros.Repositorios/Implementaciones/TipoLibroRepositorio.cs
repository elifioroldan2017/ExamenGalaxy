using Examen.GestionLibros.AccesoDatos;
using Examen.GestionLibros.Entidades;
using Examen.GestionLibros.Negocio.Implementaciones;
using Examen.GestionLibros.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.Repositorios.Implementaciones
{
    public class TipoLibroRepositorio : RepositorioBase<TipoLibro>, ITipoLibroRepositorio
    {
        public TipoLibroRepositorio(BdlibrosContext contexto) : base(contexto)
        {
        }
    }
}

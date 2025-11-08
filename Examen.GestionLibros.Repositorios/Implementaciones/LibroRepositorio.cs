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
    public class LibroRepositorio : RepositorioBase<Libro>, ILibroRepositorio
    {
        public LibroRepositorio(BdlibrosContext contexto) : base(contexto)
        {
        }
    }
}

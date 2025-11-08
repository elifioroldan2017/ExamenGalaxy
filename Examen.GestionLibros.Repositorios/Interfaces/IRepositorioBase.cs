using Examen.GestionLibros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.Negocio.Interfaces
{
    public interface IRepositorioBase<TEntidad> where TEntidad : EntidadBase
    {
        Task<ICollection<TEntidad>> ListAsync();
        Task<ICollection<TResultado>> ListAsync<TResultado>
        (
           Expression<Func<TEntidad, bool>> predicado,
           Expression<Func<TEntidad, TResultado>> selector,
          List<string> includes
        );
        Task<(ICollection<TResultado> Coleccion, int TotalRegistros)> ListAsync<TResultado, TKey>
        (
           Expression<Func<TEntidad, bool>> predicado,
           Expression<Func<TEntidad, TResultado>> selector,
           Expression<Func<TEntidad, TKey>> orderBy,
           List<string> includes,
           int pagina = 1, int filas = 10, int direcionOrder = 1
        );
        Task<TEntidad?> FindAsync(int id);
        Task<TEntidad> AddAsync(TEntidad request);
        Task UpdateAsync();
        Task DeleteAsync(int id);
    }
}

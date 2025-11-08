using Examen.GestionLibros.AccesoDatos;
using Examen.GestionLibros.Entidades;
using Examen.GestionLibros.Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.Negocio.Implementaciones
{
    public class RepositorioBase<TEntidad> : IRepositorioBase<TEntidad> where TEntidad : EntidadBase
    {
        protected readonly BdlibrosContext _contexto;
        public RepositorioBase(BdlibrosContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<ICollection<TEntidad>> ListAsync()
        {
            return await _contexto.Set<TEntidad>()
                         .Where(p => p.Activo)
                         .ToListAsync();
        }

        public async Task<ICollection<TResultado>> ListAsync<TResultado>
        (
           Expression<Func<TEntidad, bool>> predicado,
           Expression<Func<TEntidad, TResultado>> selector,
          List<string> includes
        )
        {
            IQueryable<TEntidad> query = _contexto.Set<TEntidad>()
                       .Where(predicado);

            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return await query.Select(selector)
                         .ToListAsync();
        }

        public async Task<(ICollection<TResultado> Coleccion, int TotalRegistros)> ListAsync<TResultado, TKey>
        (
           Expression<Func<TEntidad, bool>> predicado,
           Expression<Func<TEntidad, TResultado>> selector,
           Expression<Func<TEntidad, TKey>> orderBy,
           List<string> includes,
           int pagina = 1, int filas = 10, int direcionOrder = 1
        )
        {
            IQueryable<TEntidad> query = _contexto.Set<TEntidad>()
                         .Where(predicado);

            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            query = direcionOrder == 1 ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            var resultado = await query
                         .Skip((pagina - 1) * filas)
                         .Take(filas)
                         .Select(selector)
                         .ToListAsync();

            var TotalRegistro = await _contexto.Set<TEntidad>()
                         .Where(predicado)
                         .CountAsync();

            return (resultado, TotalRegistro);
        }

        public async Task<TEntidad?> FindAsync(int id)
        {
            return await _contexto.Set<TEntidad>().FirstOrDefaultAsync(p => p.Id == id && p.Activo);
        }

        public async Task<TEntidad> AddAsync(TEntidad request)
        {
            var resultado = await _contexto.Set<TEntidad>().AddAsync(request);
            await _contexto.SaveChangesAsync();
            return resultado.Entity;
        }

        public async Task UpdateAsync()
        {
            await _contexto.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _contexto.Set<TEntidad>()
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(p =>
                    p.SetProperty(p => p.Activo, false));
        }

    }
}

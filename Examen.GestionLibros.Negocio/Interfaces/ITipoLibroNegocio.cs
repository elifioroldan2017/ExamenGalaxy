using Examen.GestionLibros.DTO;
using Examen.GestionLibros.DTO.Request;
using Examen.GestionLibros.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.Negocio.Interfaces
{
   public interface ITipoLibroNegocio
    {
        Task<BaseResponse<ICollection<TipoLibroResponse>>> Listar();
        Task<BaseResponse> Eliminar(int id);

        Task<BaseResponse<TipoLibroResponse>> ObtenerPorId(int id);

        Task<BaseResponse> Actualizar(int id, TipoLibroRequest request);

        Task<BaseResponse> Registrar(TipoLibroRequest request);
    }
}

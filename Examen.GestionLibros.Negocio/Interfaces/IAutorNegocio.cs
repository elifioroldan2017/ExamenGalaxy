using Examen.GestionLibros.DTO.Request;
using Examen.GestionLibros.DTO.Response;
using Examen.GestionLibros.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.Negocio.Interfaces
{
   public interface IAutorNegocio
    {
        Task<BaseResponse<ICollection<AutorResponse>>> Listar();
        Task<BaseResponse> Eliminar(int id);

        Task<BaseResponse<AutorResponse>> ObtenerPorId(int id);

        Task<BaseResponse> Actualizar(int id, AutorRequest request);

        Task<BaseResponse> Registrar(AutorRequest request);
    }
}

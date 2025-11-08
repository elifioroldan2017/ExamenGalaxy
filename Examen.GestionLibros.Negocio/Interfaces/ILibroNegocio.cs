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
   public interface ILibroNegocio
    {
        Task<BaseResponse<ICollection<LibroResponse>>> Listar();
        Task<BaseResponse> Eliminar(int id);

        Task<BaseResponse<LibroResponse>> ObtenerPorId(int id);

        Task<BaseResponse> Actualizar(int id, LibroRequest request);

        Task<BaseResponse> Registrar(LibroRequest request);
    }
}

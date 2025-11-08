using AutoMapper;
using Examen.GestionLibros.Comun.Excepciones;
using Examen.GestionLibros.Comun;
using Examen.GestionLibros.DTO.Request;
using Examen.GestionLibros.DTO.Response;
using Examen.GestionLibros.DTO;
using Examen.GestionLibros.Entidades;
using Examen.GestionLibros.Negocio.Interfaces;
using Examen.GestionLibros.Repositorios.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.Negocio.Implementaciones
{
   public class LibroNegocio: ILibroNegocio
    {
        private readonly ILibroRepositorio _repositorio;

        private readonly ILogger<LibroNegocio> _logger;
        private readonly IMapper _mapper;

        public LibroNegocio(IMapper mapper, ILibroRepositorio repositorio, ILogger<LibroNegocio> logger)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponse<ICollection<LibroResponse>>> Listar()
        {
            BaseResponse<ICollection<LibroResponse>> respuesta =
               new BaseResponse<ICollection<LibroResponse>>();
            try
            {
                var Lista = await _repositorio.ListAsync(predicado: x => x.Activo == true,
                selector: x => new LibroResponse
                {
                    Id = x.Id,
                    FechaPublicacion=x.FechaPublicacion,
                    Autor=x.Autor.Nombres+" "+ x.Autor.Apellidos,
                    TipoLibro=x.TipoLibro.Nombre,
                    Titulo=x.Titulo
                }, includes: null
                );
                respuesta.IsSuccess = true;
                respuesta.Result = Lista;
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                _logger.LogError(ex, "{0} - {1}", respuesta.Message, ex.Message);

            }

            return respuesta;
        }


        public async Task<BaseResponse> Eliminar(int id)
        {
            var respuesta = new BaseResponse();
            try
            {
                var entidad = await _repositorio.FindAsync(id);
                if (entidad == null) throw new ExcepcionSistema("El registro no existe.", (int)CodigoErrorEnum.RegistroNoEncontrado);

                await _repositorio.DeleteAsync(id);
                respuesta.Message = "Libro eliminado correctamente";
                respuesta.IsSuccess = true;
            }
            catch (ExcepcionSistema ex)
            {
                respuesta.Message = ex.Message;
                respuesta.ErrorCode = ex.ErrorCode;
                _logger.LogError(ex, "{0} - {1}", "Hubo une error al eliminar Libro.", ex.Message);
            }
            catch (Exception ex)
            {
                respuesta.Message = "Hubo un error al eliminar Libro.";
                _logger.LogError(ex, "{0} - {1}", respuesta.Message, ex.Message);
            }
            return respuesta;
        }

        public async Task<BaseResponse<LibroResponse>> ObtenerPorId(int id)
        {
            var respuesta = new BaseResponse<LibroResponse>();
            try
            {
                var entidad = await _repositorio.FindAsync(id);
                if (entidad == null) throw new ExcepcionSistema("El registro no existe.", (int)CodigoErrorEnum.RegistroNoEncontrado);

                respuesta.Result = _mapper.Map<LibroResponse>(entidad);
                respuesta.IsSuccess = true;
            }
            catch (ExcepcionSistema ex)
            {
                respuesta.Message = ex.Message;
                respuesta.ErrorCode = ex.ErrorCode;
                _logger.LogError(ex, "{0} - {1}", "Hubo une error al obtener Libro.", ex.Message);
            }
            catch (Exception ex)
            {
                respuesta.Message = "Hubo un error al obtener Libro.";
                _logger.LogError(ex, "{0} - {1}", respuesta.Message, ex.Message);
            }
            return respuesta;
        }

        public async Task<BaseResponse> Actualizar(int id, LibroRequest request)
        {
            var respuesta = new BaseResponse();
            try
            {

                var entidad = await _repositorio.FindAsync(id);
                if (entidad == null) throw new ExcepcionSistema("El registro no existe.", (int)CodigoErrorEnum.RegistroNoEncontrado);

                _mapper.Map(request, entidad);
                await _repositorio.UpdateAsync();

                respuesta.Message = "Libro actualizar correctamente.";
                respuesta.IsSuccess = true;
            }
            catch (ExcepcionSistema ex)
            {
                respuesta.Message = ex.Message;
                respuesta.ErrorCode = ex.ErrorCode;
                _logger.LogError(ex, "{0} - {1}", "Hubo une error al actualizar Libro.", ex.Message);
            }
            catch (Exception ex)
            {
                respuesta.Message = "Hubo un error al actualizar Libro.";
                _logger.LogError(ex, "{0} - {1}", respuesta.Message, ex.Message);
            }
            return respuesta;
        }

        public async Task<BaseResponse> Registrar(LibroRequest request)
        {
            var respuesta = new BaseResponse();
            try
            {
                var resultado = await _repositorio.AddAsync(_mapper.Map<Libro>(request));
                respuesta.Message = "Libro registrado correctamente.";
                respuesta.IsSuccess = true;
            }
            catch (Exception ex)
            {
                respuesta.Message = "Hubo un error al registrar Cliente.";
                _logger.LogError(ex, "{0} - {1}", respuesta.Message, ex.Message);
            }
            return respuesta;
        }

     
    }
}

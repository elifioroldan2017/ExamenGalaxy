using AutoMapper;
using Examen.GestionLibros.Comun;
using Examen.GestionLibros.Comun.Excepciones;
using Examen.GestionLibros.DTO;
using Examen.GestionLibros.DTO.Request;
using Examen.GestionLibros.DTO.Response;
using Examen.GestionLibros.Entidades;
using Examen.GestionLibros.Negocio.Interfaces;
using Examen.GestionLibros.Repositorios.Implementaciones;
using Examen.GestionLibros.Repositorios.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.GestionLibros.Negocio.Implementaciones
{
    public class TipoLibroNegocio : ITipoLibroNegocio
    {
        private readonly ITipoLibroRepositorio _repositorio;

        private readonly ILogger<TipoLibroNegocio> _logger;
        private readonly IMapper _mapper;

        public TipoLibroNegocio(IMapper mapper,ITipoLibroRepositorio repositorio, ILogger<TipoLibroNegocio> logger)
        {
            _repositorio=repositorio;
            _mapper=mapper;
            _logger = logger;
        }

        public async Task<BaseResponse<ICollection<TipoLibroResponse>>> Listar()
        {
            BaseResponse<ICollection<TipoLibroResponse>> respuesta =
               new BaseResponse<ICollection<TipoLibroResponse>>();
            try
            {
                var Lista = await _repositorio.ListAsync(predicado: x => x.Activo == true,
                selector: x => _mapper.Map<TipoLibroResponse>(x), includes: null
                );
                respuesta.IsSuccess = true;
                respuesta.Result= Lista;
            }
            catch (Exception ex)
            {
                respuesta.Message= ex.Message;
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
                respuesta.Message = "Tipo Libro eliminado correctamente";
                respuesta.IsSuccess = true;
            }
            catch (ExcepcionSistema ex)
            {
                respuesta.Message = ex.Message;
                respuesta.ErrorCode = ex.ErrorCode;
                _logger.LogError(ex, "{0} - {1}", "Hubo une error al eliminar Tipo Libro.", ex.Message);
            }
            catch (Exception ex)
            {
                respuesta.Message = "Hubo un error al eliminar Tipo Libro.";
                _logger.LogError(ex, "{0} - {1}", respuesta.Message, ex.Message);
            }
            return respuesta;
        }

        public async Task<BaseResponse<TipoLibroResponse>> ObtenerPorId(int id)
        {
            var respuesta = new BaseResponse<TipoLibroResponse>();
            try
            {
                var entidad = await _repositorio.FindAsync(id);
                if (entidad == null) throw new ExcepcionSistema("El registro no existe.", (int)CodigoErrorEnum.RegistroNoEncontrado);

                respuesta.Result = _mapper.Map<TipoLibroResponse>(entidad);
                respuesta.IsSuccess = true;
            }
            catch (ExcepcionSistema ex)
            {
                respuesta.Message = ex.Message;
                respuesta.ErrorCode = ex.ErrorCode;
                _logger.LogError(ex, "{0} - {1}", "Hubo une error al obtener Tipo Libro.", ex.Message);
            }
            catch (Exception ex)
            {
                respuesta.Message = "Hubo un error al obtener Tipo Libro.";
                _logger.LogError(ex, "{0} - {1}", respuesta.Message, ex.Message);
            }
            return respuesta;
        }

        public async Task<BaseResponse> Actualizar(int id, TipoLibroRequest request)
        {
            var respuesta = new BaseResponse();
            try
            {

                var entidad = await _repositorio.FindAsync(id);
                if (entidad == null) throw new ExcepcionSistema("El registro no existe.", (int)CodigoErrorEnum.RegistroNoEncontrado);

                _mapper.Map(request, entidad);
                await _repositorio.UpdateAsync();

                respuesta.Message = "Cliente actualizar correctamente.";
                respuesta.IsSuccess = true;
            }
            catch (ExcepcionSistema ex)
            {
                respuesta.Message = ex.Message;
                respuesta.ErrorCode = ex.ErrorCode;
                _logger.LogError(ex, "{0} - {1}", "Hubo une error al actualizar Cliente.", ex.Message);
            }
            catch (Exception ex)
            {
                respuesta.Message = "Hubo un error al actualizar Cliente.";
                _logger.LogError(ex, "{0} - {1}", respuesta.Message, ex.Message);
            }
            return respuesta;
        }

        public async Task<BaseResponse> Registrar(TipoLibroRequest request)
        {
            var respuesta = new BaseResponse();
            try
            {
                var resultado = await _repositorio.AddAsync(_mapper.Map<TipoLibro>(request));
                respuesta.Message = "Cliente registrado correctamente.";
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

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
   public class AutorNegocio: IAutorNegocio
    {

        private readonly IAutorRepositorio _repositorio;
        private readonly ILogger<AutorNegocio> _logger;
        private readonly IMapper _mapper;

        public AutorNegocio(IMapper mapper, IAutorRepositorio repositorio, ILogger<AutorNegocio> logger)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponse<ICollection<AutorResponse>>> Listar()
        {
            BaseResponse<ICollection<AutorResponse>> respuesta =
               new BaseResponse<ICollection<AutorResponse>>();
            try
            {
                var Lista = await _repositorio.ListAsync(predicado: x => x.Activo == true,
                selector: x => _mapper.Map<AutorResponse>(x), includes: null
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
                respuesta.Message = "Autor eliminado correctamente";
                respuesta.IsSuccess = true;
            }
            catch (ExcepcionSistema ex)
            {
                respuesta.Message = ex.Message;
                respuesta.ErrorCode = ex.ErrorCode;
                _logger.LogError(ex, "{0} - {1}", "Hubo une error al eliminar Autor.", ex.Message);
            }
            catch (Exception ex)
            {
                respuesta.Message = "Hubo un error al eliminar Autor.";
                _logger.LogError(ex, "{0} - {1}", respuesta.Message, ex.Message);
            }
            return respuesta;
        }

        public async Task<BaseResponse<AutorResponse>> ObtenerPorId(int id)
        {
            var respuesta = new BaseResponse<AutorResponse>();
            try
            {
                var entidad = await _repositorio.FindAsync(id);
                if (entidad == null) throw new ExcepcionSistema("El registro no existe.", (int)CodigoErrorEnum.RegistroNoEncontrado);

                respuesta.Result = _mapper.Map<AutorResponse>(entidad);
                respuesta.IsSuccess = true;
            }
            catch (ExcepcionSistema ex)
            {
                respuesta.Message = ex.Message;
                respuesta.ErrorCode = ex.ErrorCode;
                _logger.LogError(ex, "{0} - {1}", "Hubo une error al obtener Autor.", ex.Message);
            }
            catch (Exception ex)
            {
                respuesta.Message = "Hubo un error al obtener Tipo Libro.";
                _logger.LogError(ex, "{0} - {1}", respuesta.Message, ex.Message);
            }
            return respuesta;
        }

        public async Task<BaseResponse> Actualizar(int id, AutorRequest request)
        {
            var respuesta = new BaseResponse();
            try
            {

                var entidad = await _repositorio.FindAsync(id);
                if (entidad == null) throw new ExcepcionSistema("El registro no existe.", (int)CodigoErrorEnum.RegistroNoEncontrado);

                _mapper.Map(request, entidad);
                await _repositorio.UpdateAsync();

                respuesta.Message = "Autor actualizar correctamente.";
                respuesta.IsSuccess = true;
            }
            catch (ExcepcionSistema ex)
            {
                respuesta.Message = ex.Message;
                respuesta.ErrorCode = ex.ErrorCode;
                _logger.LogError(ex, "{0} - {1}", "Hubo une error al actualizar Autor.", ex.Message);
            }
            catch (Exception ex)
            {
                respuesta.Message = "Hubo un error al actualizar Autor.";
                _logger.LogError(ex, "{0} - {1}", respuesta.Message, ex.Message);
            }
            return respuesta;
        }

        public async Task<BaseResponse> Registrar(AutorRequest request)
        {
            var respuesta = new BaseResponse();
            try
            {
                TipoLibro o = new TipoLibro();
                var resultado = await _repositorio.AddAsync(_mapper.Map<Autor>(request));
                respuesta.Message = "Autor registrado correctamente.";
                respuesta.IsSuccess = true;
            }
            catch (Exception ex)
            {
                respuesta.Message = "Hubo un error al registrar Autor.";
                _logger.LogError(ex, "{0} - {1}", respuesta.Message, ex.Message);
            }
            return respuesta;
        }
    }
}

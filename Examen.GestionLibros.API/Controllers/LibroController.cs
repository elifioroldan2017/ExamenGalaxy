using Examen.GestionLibros.DTO.Request;
using Examen.GestionLibros.Negocio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examen.GestionLibros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ILibroNegocio _Repositorio;
        public LibroController(ILibroNegocio Repositorio)
        {
            _Repositorio = Repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Repositorio.Listar();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> FindByID(int Id)
        {
            var result = await _Repositorio.ObtenerPorId(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _Repositorio.Eliminar(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(LibroRequest request)
        {
            var result = await _Repositorio.Registrar(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, LibroRequest request)
        {
            var result = await _Repositorio.Actualizar(id, request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}

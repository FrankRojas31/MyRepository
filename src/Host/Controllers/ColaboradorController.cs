using ApplicationCore.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorService _service;

        public ColaboradorController(IColaboradorService service)
        {
            _service = service;
        }

        [HttpGet("ConFiltros")]
        public async Task<IActionResult> GetColaboradorConFiltros([FromQuery]DateTime fechaInicio, DateTime fechaFin, bool esProfesor)
        {
            var results = await _service.GetListColaboradores(fechaInicio, fechaFin, esProfesor);

            return Ok(results);
        }

        [HttpPost("CrearColaborador")]
        public async Task<IActionResult> PostColaborador([FromBody] Colaboradores colaboradores)
        {
            var results = await _service.PostColaborador(colaboradores);

            return Ok(results);
        }
    }
}

using ApplicationCore.Commands;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _service;
        private readonly IMediator _mediator;
        public EstudianteController(IEstudianteService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [HttpGet("ObtenerDatos")]
        public async Task<ActionResult<Response<object>>> GetData()
        {
            var result = await _service.ObtenerDatos();
            return Ok(result);
        }

        [HttpGet("ObtenerDatoPorId")]
        public async Task<ActionResult<Response<object>>> GetById(int id)
        {
            var result = await _service.ObtenerDatoPorId(id);
            return Ok(result);
        }

        [HttpPost("InsertarDatos")]
        public async Task<ActionResult<Response<object>>> Insert([FromBody] EstudianteDTO request)
        {
            var result = await _service.InsertarDatos(request);
            return Ok(result);
        }

        [HttpPut("ActualizarDato")]
        public async Task<ActionResult<Response<object>>> Update(int id, [FromBody] EstudianteDTO request)
        {
            var result = await _service.ActualizarDato(id, request);
            return Ok(result);
        }

        [HttpDelete("EliminarDato")]
        public async Task<ActionResult<Response<object>>> Delete(int id, bool BorradoLogico)
        {
            var result = await _service.EliminarDato(id, BorradoLogico);
            return Ok(result);
        }

        [HttpPost("CreateErika")]
        public async Task<ActionResult<Response<int>>> CreateEstudiante(EstudianteCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("pdf")] 
        public async Task<ActionResult> GetPdf()
        {
            var pdf = await _service.GetPDF();
            return File(pdf, "application/pdf", "Estudiantes.pdf");
        }
        
    }
}
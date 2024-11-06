using ApplicationCore.DTOs;
using ApplicationCore.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IEstudianteService
    {
        Task<Response<object>> ObtenerDatos();
        Task<Response<EstudianteDTO>> InsertarDatos(EstudianteDTO estudianteDTO);
        Task<Response<object>> ObtenerDatoPorId(int id);
        Task<Response<object>> ActualizarDato(int id, EstudianteDTO estudianteDTO);
        Task<Response<object>> EliminarDato(int id, bool BorradoLogico);
        Task<byte[]> GetPDF();
    }
}

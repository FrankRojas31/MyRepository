using ApplicationCore.Wrappers;
using Domain.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IColaboradorService
    {
        Task<Response<object>> GetListColaboradores(DateTime fechaInicio, DateTime fechaFin, bool esProfesor);
        Task<Response<object>> PostColaborador(Colaboradores colaboradores);
    }
}

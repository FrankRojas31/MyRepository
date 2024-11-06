using ApplicationCore.Commands;
using ApplicationCore.Wrappers;
using AutoMapper;
using Infraestructure.Persistence;
using MediatR;

namespace Infraestructure.EventHandlers.Estudiantes
{
    public class CreateEstudiantesHandler : IRequestHandler<EstudianteCreateCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateEstudiantesHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(EstudianteCreateCommand request, CancellationToken cancellationToken)
        {
            var e = new EstudianteCreateCommand();

            e.nombre = request.nombre;
            e.edad = request.edad;
            e.correo = request.correo;
            e.IsDeleted = false;

            var estudiante = _mapper.Map<Domain.Entities.Estudiantes>(e);
            await _context.Estudiantes.AddAsync(estudiante);
            await _context.SaveChangesAsync(cancellationToken);
            return new Response<int>(estudiante.Id, "Registro exitoso");
        }
    }
}

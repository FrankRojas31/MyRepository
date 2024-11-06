using ApplicationCore.Commands;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Mappings
{
    public class Estudiante : Profile
    {
        public Estudiante()
        {
            CreateMap<EstudianteCreateCommand, Domain.Entities.Estudiantes>().ForMember(x => x.Id, y => y.Ignore());
        }
    }
}

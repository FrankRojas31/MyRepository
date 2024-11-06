using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class EstudiantePDFDTO
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public virtual ICollection<EstudianteDTO> Estudiantes { get; set; } = new List<EstudianteDTO>();

    }
}
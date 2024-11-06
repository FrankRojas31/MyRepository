using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class EstudianteDTO
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public int? edad { get; set; }
        public string correo { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
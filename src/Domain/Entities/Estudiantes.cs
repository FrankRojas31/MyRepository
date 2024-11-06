using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Estudiantes")]
    public class Estudiantes
    {
        [Key]
        public int Id { get; set; }
        public string nombre { get; set; }
        public int? edad { get; set; }
        public string correo { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
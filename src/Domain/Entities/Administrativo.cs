using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Tbl_Administrativo")]
    public class Administrativo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int FkColaborador { get; set; }

        [ForeignKey("FkColaborador")]
        public virtual Colaboradores Colaborador { get; set; }

        public string correo { get; set; }

        public string puesto { get; set; }

        public decimal nomina { get; set; }
    }
}
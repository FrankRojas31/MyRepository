using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Tbl_Profesor")]
    public class Profesor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int FkColaborador { get; set; }

        [ForeignKey("FkColaborador")]
        public virtual Colaboradores Colaborador { get; set; }

        public string correo { get; set; }

        public string departamento { get; set; }
    }
}
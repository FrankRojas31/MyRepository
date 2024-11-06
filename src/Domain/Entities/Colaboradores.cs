using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Tbl_Colaboradores")]
    public class Colaboradores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string nombre { get; set; }

        public int edad { get; set; }

        public DateTime cumpleaños { get; set; }

        public bool esProfesor { get; set; }

        public DateTime fechaCreacion { get; set; }

        //public virtual ICollection<Profesor> Profesores { get; set; }
        // public virtual ICollection<Administrativo> Administrativos { get; set; }
    }
}
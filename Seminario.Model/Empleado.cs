using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.Model
{
    [Table("Empleado")]
    public class Empleado : Entity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual int EmpleadoId { get; set; }
        public virtual string Legajo { get; set; }
        public virtual string Nombre { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.Model
{
    public class Empleado : Entity
    {
        public virtual string Nombre { get; set; }
        public virtual string Legajo { get; set; }
    }
}

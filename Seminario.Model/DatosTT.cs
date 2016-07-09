using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.Model
{
    public class DatosTT : Entity
    {
        public virtual int Plazo { get; set; }
        public virtual float TasaVigente { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual float Sellado { get; set; }
    }
}

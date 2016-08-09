using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.Model
{
    public class Producto : Entity
    {
        public virtual string Nombre { get; set; }
        public virtual int CodigoProducto { get; set; }
        public virtual IList<DatosTT> DatosTT { get; set;}
    }
}

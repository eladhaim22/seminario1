using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.WebServices.Contracts
{
    public class ProductoDto : EntityDto
    {
        public string Nombre { get; set; }
        public int CodigoProducto { get; set; }
        public IList<DatosTTDto> DatosTT { get; set; }
    }
}

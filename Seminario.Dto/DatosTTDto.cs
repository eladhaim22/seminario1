using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.Dto
{
    public class ProductoDto : EntityDto
    {
        public int Plazo { get; set; }
        public float TasaVigente { get; set; }
        public ProductoDto Producto { get; set; }
    }
}

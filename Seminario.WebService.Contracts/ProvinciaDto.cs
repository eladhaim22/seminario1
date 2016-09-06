using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.WebServices.Contracts
{
    public class ProvinciaDto : EntityDto
    {
        public string Nombre { get; set; }
        public float Sellado { get; set; }
    }
}

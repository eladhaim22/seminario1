using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.WebServices.Contracts
{
    public class DatosTTDto : EntityDto
    {
        public int Plazo { get; set; }
        public decimal TasaVigente { get; set; }
        
    }
}

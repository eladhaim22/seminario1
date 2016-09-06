using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.WebServices.Contracts
{
    public class EmpleadoDto : EntityDto
    {
        public int EmpleadoId { get; set; }
        public string Legajo { get; set; }
        public string Nombre { get; set; }
    }
}

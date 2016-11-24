using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.WebServices.Contracts
{
    public class ChequeDto : EntityDto
    {       
 
        public int OtrosDias {get; set;}
        public TipoBancoDto Banco { get; set; }
        public DateTime FechaAcreditacion { get; set; }
        public float Importe { get; set; }
        public int Plazo { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Nosis { get; set; }
        public float TEOps { get; set; }
        public float TEAdelantada { get; set; }
        public float TNAA { get; set; }
        public float Interes { get; set; }
        public float Comision { get; set; }
        public float Sellado { get; set; }
        public float IVA { get; set; }
        public float Spread { get; set; }
        public float CFT { get; set; }
        public float CFTMes { get; set; }
        public float NetoLiquidar { get; set; }
        public float Ponderado { get; set; }
        public float TETT { get; set; }
        public float TEATT { get; set; }
        public float IIBB { get; set; }
        public float Costo { get; set; }
        public float Neto { get; set; }
    }
}

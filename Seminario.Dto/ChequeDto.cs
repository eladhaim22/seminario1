using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.Dto
{
    public class ChequeDto : EntityDto
    {
        public DateTime FechaAcreditacion { get; set; }
        public float Importe { get; set; }
        public int Plazo { get; set; }
        public string CuitEmisor { get; set; }
        public string NombreEmisor { get; set; }
        public string EstadoNosisEmisor { get; set; }
        public float TE { get; set; }
        public float TEA { get; set; }
        public float TNAA { get; set; }
        public float Interes { get; set; }
        public float Comision { get; set; }
        public float Sellado { get; set; }
        public float IVA { get; set; }
        public float GastoTotal { get; set; }
        public float Spread { get; set; }
        public float CFT { get; set; }
        public float CFTMes { get; set; }
        public float NetoLiquidar { get; set; }
        public float ImportePonderado { get; set; }
        public float TETT { get; set; }
        public float TEATT { get; set; }
        public float IIBB { get; set; }
        public float Costo { get; set; }
        public float Neto { get; set; }
    }
}

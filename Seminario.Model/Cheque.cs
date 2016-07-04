using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Seminario.Model
{
    public class Cheque : IEntity
    {
        public virtual DateTime FechaAcreditacion { get; set; }
        public virtual float Importe { get; set; }
        public virtual int Plazo { get; set; }
        public virtual string CuitEmisor { get; set; }
        public virtual string NombreEmisor { get; set; }
        public virtual string EstadoNosisEmisor { get; set; }
        public virtual float TE { get; set; }
        public virtual float TEA { get; set; }
        public virtual float TNAA { get; set; }
        public virtual float Interes { get; set; }
        public virtual float Comision { get; set; }
        public virtual float Sellado { get; set; }
        public virtual float IVA { get; set; }
        public virtual float GastoTotal { get; set; }
        public virtual float Spread { get; set; }
        public virtual float CFT { get; set; }
        public virtual float CFTMes { get; set; }
        public virtual float NetoLiquidar { get; set; }
        public virtual float ImportePonderado { get; set; }
        public virtual float TETT { get; set; }
        public virtual float TEATT { get; set; }
        public virtual float IIBB { get; set; }
        public virtual float Costo { get; set; }
        public virtual float Neto { get; set; }

    }
}

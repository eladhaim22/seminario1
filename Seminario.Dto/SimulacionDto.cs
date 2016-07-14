using Seminario.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.Model
{
    
    public class SimulacionDto : EntityDto
    {
        public string CuitCliente { get; set; }
        public string TorCliente { get; set; }
        public DateTime FechaDescuento { get; set; }
        public float ValorNominal { get; set; }
        public float Intereses { get; set; }
        public float Comision { get; set; }
        public float Sellado { get; set; }
        public float Iva { get; set; }
        public float GastoTotal { get; set; }
        public float TT { get; set; }
        public float TNAV { get; set; }
        public float NetoLiquidar { get; set; }
        public float ImportePonderadoTotal { get; set; }
        public float TipoCateg { get; set; }
        public int CantidadChques { get; set; }
        public int CodProd { get; set; }
        public float FechaVencimientoPond { get; set; }
        public float SpreadTotal { get; set; }
        public float NetoTotal { get; set; }
        public float TasaIIBB { get; set; }
        public float TasaIva { get; set; }
        public float TasaSellado { get; set; }
        public string Estado { get; set; }
        public int Legajo { get; set; }
        public List<ChequeDto> Cheques { get; set; }
    }
}

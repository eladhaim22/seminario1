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
        public float ImporteTotal { get; set; }
        public float InteresTotal { get; set; }
        public float ComisionTotal { get; set; }
        public float SelladoTotal { get; set; }
        public float IvaTotal { get; set; }
        public float GastoTotal { get; set; }
        public float TT { get; set; }
        public float TNAV { get; set; }
        public float NetoLiquidarTotal { get; set; }
        public float ImportePonderadoTotal { get; set; }
        public float TipoCateg { get; set; }
        public int ProductoId { get; set; }
        public DateTime FechaVencimientoPond { get; set; }
        public string CantidadCompra { get; set; }
        public float SpreadTotal { get; set; }
        public float NetoTotal { get; set; }
        public float TasaIIBB { get; set; }
        public float TasaIva { get; set; }
        public float TasaSellado { get; set; }
        public string Estado { get; set; }
        public int EmpleadoId { get; set; }
        public float ComisionAdministrativa { get; set; }
        public int ProvinciaId { get; set; }
        public List<ChequeDto> Cheques { get; set; }
    }
}

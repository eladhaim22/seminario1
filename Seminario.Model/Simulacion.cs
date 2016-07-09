using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.Model
{
    public class Simulacion : Entity
    {
        public virtual string CuitCliente { get; set; }
        public virtual string TorCliente { get; set; }
        public virtual DateTime FechaDescuento { get; set; }
        public virtual float ImporteTotal { get; set; }
        public virtual float InteresTotal { get; set; }
        public virtual float ComisionTotal { get; set; }
        public virtual float SelladoTotal { get; set; }
        public virtual float IvaTotal { get; set; }
        public virtual float GastoTotal { get; set; }
        public virtual float TT { get; set; }
        public virtual float TNAV { get; set; }
        public virtual float NetoLiquidarTotal { get; set; }
        public virtual float ImportePonderadoTotal { get; set; }
        public virtual float TipoCateg { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual float FechaVencimientoPond { get; set; }
        public virtual string CantidadCompra { get; set; }
        public virtual float SpreadTotal { get; set; }
        public virtual float NetoTotal { get; set; }
        public virtual float TasaIIBB { get; set; }
        public virtual float TasaIva { get; set; }
        public virtual float TasaSellado { get; set; }
        public virtual string Estado { get; set; }
        public virtual float ComisionAdministrativa { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual Provincia Provincia { get; set; }
        public virtual List<Cheque> Cheques { get; set; }
    }
}

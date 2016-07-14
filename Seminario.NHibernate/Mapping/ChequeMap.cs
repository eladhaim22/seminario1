using FluentNHibernate.Mapping;
using Seminario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seminario.Model;

namespace Seminario.NHibernate.Mapping
{ 
    public class ChequeMap : ClassMap<Cheque>
    {
        public ChequeMap(){
            Id(x => x.Id);
            Map(x => x.CFT);
            Map(x => x.CFTMes);
            Map(x => x.Comision);
            Map(x => x.Costo);
            Map(x => x.CuitEmisor);
            Map(x => x.EstadoNosisEmisor);
            Map(x => x.FechaAcreditacion);
            Map(x => x.IIBB);
            Map(x => x.Importe);
            Map(x => x.ImportePonderado);
            Map(x => x.Interes);
            Map(x => x.IVA);
            Map(x => x.Neto);
            Map(x => x.NetoLiquidar);
            Map(x => x.NombreEmisor);
            Map(x => x.Plazo);
            Map(x => x.Sellado);
            Map(x => x.Spread);
            Map(x => x.TE);
            Map(x => x.TEA);
            Map(x => x.TEATT);
            Map(x => x.TETT);
            Map(x => x.TNAA);

        }
    }
}

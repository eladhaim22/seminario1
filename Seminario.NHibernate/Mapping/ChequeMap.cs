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
            Map(x => x.OtrosDias, "otrosDias");
            Map(x => x.Banco, "banco");
            Map(x => x.CFT,"cft");
            Map(x => x.CFTMes,"cftMes");
            Map(x => x.Comision,"comision");
            Map(x => x.Costo,"costo");
            Map(x => x.NombreEmisor, "nombreEmisor");
            Map(x => x.CuitEmisor,"cuitEmisor");
            Map(x => x.EstadoNosisEmisor,"estadoNosisEmisor");
            Map(x => x.FechaAcreditacion,"fechaAcreditacion");
            Map(x => x.IIBB,"IIBB");
            Map(x => x.Importe,"importe");
            Map(x => x.ImportePonderado,"importePonderado");
            Map(x => x.Interes,"interes");
            Map(x => x.IVA,"iva");
            Map(x => x.Neto,"neto");
            Map(x => x.NetoLiquidar,"netoLiquidar");
            Map(x => x.Plazo,"plazo");
            Map(x => x.Sellado,"sellado");
            Map(x => x.Spread,"spread");
            Map(x => x.TE,"TE");
            Map(x => x.TEA,"TEA");
            Map(x => x.TNAA, "TNAA");
            Map(x => x.TEATT,"TEATT");
            Map(x => x.TETT,"TETT");
        }
    }
}

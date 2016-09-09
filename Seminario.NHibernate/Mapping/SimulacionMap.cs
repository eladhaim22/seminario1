using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Seminario.Model;

namespace Seminario.NHibernate.Mapping
{
	public class SimulacionMap : ClassMap<Simulacion>
	{
		public SimulacionMap()
		{
			Id(x => x.Id, "idSimulacion");
			Map(x => x.CuitCliente, "cuitCliente");
			Map(x => x.TorCliente, "torCliente");
			Map(x => x.FechaDescuento, "fechaDescuento");
			Map(x => x.ImporteTotal, "importeTotal");
			Map(x => x.InteresTotal, "interesTotal");
			Map(x => x.ComisionTotal, "comisionTotal");
			Map(x => x.SelladoTotal, "selladoTotal");
			Map(x => x.IvaTotal, "ivaTotal");
			Map(x => x.GastoTotal, "gastoTotal");
			Map(x => x.TT, "TT");
			Map(x => x.TNAV, "TNAV");
			Map(x => x.NetoLiquidarTotal, "netoLiquidarTotal");
			Map(x => x.ImportePonderadoTotal, "importePonderadoTotal");
			Map(x => x.TipoCategoria, "tipoCategoria");
			References(x => x.Producto, "idProducto");
			Map(x => x.FechaVencimientoPond, "fechaVencimientoPond");
			Map(x => x.SpreadTotal, "spreadTotal");
			Map(x => x.NetoTotal, "netoTotal");
			Map(x => x.TasaIIBB, "tasaIIBB");
			Map(x => x.TasaIva, "tasaIva");
			Map(x => x.TasaSellado, "tasaSellado");
			Map(x => x.Estado, "estado");
			Map(x => x.FechaCreacion, "fechaCreacion");
			Map(x => x.FechaUltimaModificacion, "fechaUltimaModificacion");
			References(x => x.Empleado, "legajo");
			References(x => x.Provincia, "idProvincia");
			HasMany(x => x.Cheques).KeyColumn("idSimulacion")
			.Cascade.All();
		}
	}
}

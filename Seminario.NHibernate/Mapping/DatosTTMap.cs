using FluentNHibernate.Mapping;
using Seminario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.NHibernate.Mapping
{
    public class DatosTTMap : ClassMap<DatosTT>
    {
        public DatosTTMap()
        {
            Id(x => x.Id,"idDatosTT");
            Map(x => x.Plazo,"plazo");
            Map(x => x.TasaVigente,"tasaVigente");
            References(x => x.Producto).Column("idProducto");
        }
    }
}

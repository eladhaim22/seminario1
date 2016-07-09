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
            Id(x => x.Id);
            Map(x => x.Plazo);
            Map(x => x.TasaVigente);
            Map(x => x.Sellado);
        }
    }
}

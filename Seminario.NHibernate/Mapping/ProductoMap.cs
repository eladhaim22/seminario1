using FluentNHibernate.Mapping;
using Seminario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.NHibernate.Mapping
{
    public class ProductoMap : ClassMap<Producto>
    {
        public ProductoMap()
        {
            Id(x => x.Id,"codProd");
            Map(x => x.Nombre); 
        }
    }
}

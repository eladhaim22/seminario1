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
            Id(x => x.Id,"idProducto");
            Map(x => x.Nombre,"nombre");
            Map(x => x.CodigoProducto, "codigo");
            HasMany(x => x.DatosTT).KeyColumn("idProducto").Cascade.All(); 
        }
    }
}

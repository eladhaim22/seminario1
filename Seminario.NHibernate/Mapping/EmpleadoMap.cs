using FluentNHibernate.Mapping;
using Seminario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.NHibernate.Mapping
{
    public class EmpleadoMap : ClassMap<Empleado>
    {
        public EmpleadoMap()
        {
            Table("Empleado");
            Id(x => x.EmpleadoId,"EmpleadoId");
            Map(x => x.Legajo, "Legajo");
            Map(x => x.Nombre, "Nombre");
        }
    }
}

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
            Table("UserProfiles");
            Id(x => x.Id,"UserId");
            Map(x => x.Legajo, "legajo");
            Map(x => x.Nombre, "UserName");
        }
    }
}

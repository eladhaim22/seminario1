using FluentNHibernate.Mapping;
using Seminario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.NHibernate.Mapping
{ 
    public class ChequeClassMapping : ClassMap<Cheque>
    {
        public ChequeClassMapping(){
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Amount);
        }
    }
}

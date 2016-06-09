namespace Seminario.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Cheque : IEntity
    {
        public virtual int Id { get; set; }
        public virtual float Amount 
        { 
            get; 
            set; 
        }

        public virtual string Name 
        { 
            get; 
            set; 
        }

    }
}

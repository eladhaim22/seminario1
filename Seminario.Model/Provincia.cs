﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.Model
{
    public class Provincia : Entity
    {
        public virtual string Nombre { get; set; }
        public virtual float Sellado { get; set; }
    }
}

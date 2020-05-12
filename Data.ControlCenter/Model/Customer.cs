using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Model
{
   public class Customer : DomainObject<int>
    {
        public virtual string CustomerName { get; set; }
        public virtual string Address { get; set; }
        public virtual string Tel { get; set; }
        public virtual decimal Balance { get; set; }
        // llamada a la clase  LogActualizacion en Carpeta General\LogActualizacion.cs
        public virtual LogActualizacion Log { get; set; }
    }
}

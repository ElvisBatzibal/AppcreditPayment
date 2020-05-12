using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Model
{
    public class CreditAccount : DomainObject<int>
    {
        public virtual int AccountNum { get; set; }
        public virtual decimal DocTotal { get; set; }

        public virtual LogActualizacion Log { get; set; }
    }
}

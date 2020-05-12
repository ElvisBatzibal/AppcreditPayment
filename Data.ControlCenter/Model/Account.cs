using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Model
{
    public class Account : DomainObject<int>
    {
        //public virtual string CardCode
        public virtual string AccountName { get; set; }
        public virtual string AccountRef { get; set; }
        public virtual nCurrency Currency { get; set; }
        public virtual int CardCode { get; set; }
        public virtual decimal InitialBalance { get; set; }
        public virtual decimal PaidToDate { get; set; }

        public virtual string Status { get; set; }
        // llamada a la clase  LogActualizacion en Carpeta General\LogActualizacion.cs
        public virtual LogActualizacion Log { get; set; }
    }
    public enum nCurrency
    {
        QTZ,
        USD
    }
}

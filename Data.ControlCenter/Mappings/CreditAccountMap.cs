using Data.ControlCenter.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Mappings
{
   public class CreditAccountMap : ClassMap<CreditAccount>
    {
        /// <summary>
        /// Mapeo  Modelo CreditAccount
        /// </summary>
        public CreditAccountMap()
        {

            Id(x => x.EntityID).Column("TransactionId").GeneratedBy.Identity();
            Map(x => x.AccountNum);
            Map(x => x.DocTotal);
         
            Component(x => x.Log, m =>//Mapeo componente LogActualizacion en carpeta 
            {
                m.Map(x => x.Active);
                m.Map(x => x.CreationDate);
                m.Map(x => x.IdHostCreation);
                m.Map(x => x.UserCreation);
                m.Map(x => x.UpdateDate);
                m.Map(x => x.UserUpdate);
            });
        }
    }
}

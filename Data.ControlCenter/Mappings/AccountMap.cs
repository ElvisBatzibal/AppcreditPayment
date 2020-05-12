using Data.ControlCenter.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Mappings
{
    public class AccountMap : ClassMap<Account>
    {
        /// <summary>
        /// Mapeo  Modelo Usuario en carpeta Model\Usuario.cs
        /// </summary>
        public AccountMap()
        {

            Id(x => x.EntityID).Column("AccountNum").GeneratedBy.Identity();
            Map(x => x.AccountName);
            Map(x => x.AccountRef);
            Map(x => x.Currency);
            Map(x => x.CardCode);
            Map(x => x.InitialBalance);
            Map(x => x.PaidToDate);
            Map(x => x.Status);

            Component(x => x.Log, m =>//Mapeo componente LogActualizacion en carpeta Model\Usuario.cs
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

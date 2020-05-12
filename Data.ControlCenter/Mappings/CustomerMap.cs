using Data.ControlCenter.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Mappings
{
   public class CustomerMap: ClassMap<Customer>
    {
        /// <summary>
        /// Mapeo  Modelo Usuario en carpeta Model\Usuario.cs
        /// </summary>
        public CustomerMap()
        {

            Id(x => x.EntityID).Column("CardCode").GeneratedBy.Identity();
            Map(x => x.CustomerName);
            Map(x => x.Address);
            Map(x => x.Tel);
            Map(x => x.Balance);         

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

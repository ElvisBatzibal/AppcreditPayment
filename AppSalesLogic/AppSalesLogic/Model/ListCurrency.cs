using System;
using System.Collections.Generic;
using System.Text;

namespace AppSalesLogic.Model
{
    public class ListCurrency
    {
        public DomainModels.Models.AccountDTO.nCurrency Cur { get; set; }
        public ListCurrency()
        {

        }
        public static List<ListCurrency> GetList()
        {
            var ListReg = new List<ListCurrency>();
            ListReg.Add(new ListCurrency { Cur = DomainModels.Models.AccountDTO.nCurrency.QTZ });
            ListReg.Add(new ListCurrency { Cur = DomainModels.Models.AccountDTO.nCurrency.USD });

            return ListReg;
        }
    }
}

using AppSalesLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSalesLogic.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}

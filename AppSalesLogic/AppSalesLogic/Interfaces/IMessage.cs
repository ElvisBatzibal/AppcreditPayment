using System;
using System.Collections.Generic;
using System.Text;

namespace AppSalesLogic.Interfaces
{
    public interface IMessage
    {
        void LongTime(string message);
        void ShortTime(string message);
    }
}

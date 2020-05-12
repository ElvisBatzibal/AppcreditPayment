using System;
using System.Collections.Generic;
using System.Text;

namespace AppSalesLogic.Model
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public bool IsReachable { get; set; }
        public string Message { get; set; }
        public Object Result { get; set; }
    }
}
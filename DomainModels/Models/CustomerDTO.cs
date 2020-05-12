using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainModels.Models
{
    [JsonObject]
    public class CustomerDTO
    {
        public int EntityID { get; set; }
        public  string CustomerName { get; set; }
        public  string Address { get; set; }
        public  string Tel { get; set; }
        public  decimal Balance { get; set; }
        public LogActualizacionDTO Log { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int InternalKey { get; set; }

        public CustomerDTO()
        {
            Log = new LogActualizacionDTO();
            CustomerName = "";
            Address = "";
            Tel = "";
        }
    }
}
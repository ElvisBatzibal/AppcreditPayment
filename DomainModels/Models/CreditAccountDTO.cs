using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainModels.Models
{
    [JsonObject]
    public class CreditAccountDTO
    {
        public int EntityID { get; set; }
        public  int AccountNum { get; set; }
        public  decimal DocTotal { get; set; }
        public LogActualizacionDTO Log { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int InternalKey { get; set; }
        public CreditAccountDTO()
        {
            Log = new LogActualizacionDTO();
        }
    }
}
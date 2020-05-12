using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainModels.Models
{
    [JsonObject]
    public class AccountUserDTO
    {
        public int EntityID { get; set; }
        public  string UserName { get; set; }
        public  string Password { get; set; }        
        public  LogActualizacionDTO Log { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int InternalKey { get; set; }
        public AccountUserDTO()
        {
            Log = new LogActualizacionDTO();
        }
    }
}
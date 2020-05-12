using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainModels.Models
{
    [JsonObject]
    public class LogActualizacionDTO
    {
        public string IdHostCreation { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserCreation { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UserUpdate { get; set; }
        public bool Active { get; set; }
        public LogActualizacionDTO()
        {
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }
    }

}

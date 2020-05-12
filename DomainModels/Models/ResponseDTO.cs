using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainModels.Models
{
    [JsonObject]
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int InternalKey { get; set; }

        public ResponseDTO()
        {
            Success = false;
        }

    }
}
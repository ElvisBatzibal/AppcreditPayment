using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DomainModels.Models
{
    [JsonObject]
    public class AccountDTO
    {
        public int EntityID { get; set; }
        public LogActualizacionDTO Log { get; set; }
        public string AccountName { get; set; }
        public string AccountRef { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public nCurrency Currency { get; set; }
        public int CardCode { get; set; }
        public decimal InitialBalance { get; set; }
        public decimal PaidToDate { get; set; }
        public decimal PaidOpenToDate { get; set; }

        public string Status { get; set; }

        public List<CreditAccountDTO> Credits { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int InternalKey { get; set; }

        public AccountDTO()
        {
            Log = new LogActualizacionDTO();
            Credits = new List<CreditAccountDTO>();
            Currency = nCurrency.QTZ;
        }
        public enum nCurrency
        {
            [EnumMember(Value = "QTZ")]
            QTZ, 
            [EnumMember(Value = "USD")]
            USD
        }
    }
}
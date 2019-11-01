using System;
using Newtonsoft.Json;

namespace MasterAgg.Models
{
    public class SOURCE_LOG
    {
        [JsonProperty("Log_UID")]
        public Guid Log_UID { get; set; }
        [JsonProperty("Batch_UID")]
        public Guid Batch_UID { get; set; }
        [JsonProperty("Publisher")]
        public string Publisher { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("Category")]
        public string Category { get; set; }
        [JsonProperty("Message")]
        public string Message { get; set; }
        [JsonProperty("BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [JsonProperty("Dataset")]
        public string Dataset { get; set; }
        [JsonProperty("Elapsed")]
        public TimeSpan Elapsed { get; set; }
        [JsonProperty("Source_UID")]
        public Guid Source_UID { get; set; }
        [JsonProperty("File_UID")]
        public Guid File_UID { get; set; }
        [JsonProperty("FileName")]
        public string Filename { get; set; }
        [JsonProperty("Pack_UID")]
        public Guid? Pack_UID { get; set; }
        [JsonProperty("IPack_UID")]
        public Guid? iPack_UID { get; set; }
        [JsonProperty("AccountToken")]
        public string AccountToken { get; set; }
        [JsonProperty("Account_UID")]
        public Guid? Account_UID { get; set; }
        [JsonProperty("Security_UID")]
        public Guid? Security_UID { get; set; }
        [JsonProperty("Position_UID")]
        public Guid? Position_UID { get; set; }
        [JsonProperty("Transaction_UID")]
        public Guid? Transaction_UID { get; set; }
        [JsonProperty("Lot_UID")]
        public Guid? Lot_UID { get; set; }
        [JsonProperty("Work_UID")]
        public Guid? Work_UID { get; set; }
        [JsonProperty("ExternalID")]
        public string ExternalID { get; set; }
        [JsonProperty("LogDate")]
        public DateTime LogDate { get; set; }
    }
}

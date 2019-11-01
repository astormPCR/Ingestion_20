using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MasterAgg.Models
{
    public class SourceFileDefinition
    {
        [JsonProperty("File_UID")]
        public Guid File_uid { get; set; }
        [JsonProperty("Column")]
        public string ColumnName { get; set; }
        [JsonProperty("Index")]
        public int columnIndex { get; set; }
        [JsonProperty("Target")]
        public Guid? target { get; set; }
        [JsonProperty("Datatype")]
        public string datatype { get; set; }
        [JsonProperty("PII")]
        public bool pii { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FeedHandlerWatcher
{
    public class SourceFileConfiguration
    {
        [JsonProperty("File_UID")]
        public Guid File_UID { get; set; }
        [JsonProperty("Source_UID")]
        public Guid Source_UID { get; set; } 
        [JsonProperty("DatasetID")]
        public string Dataset { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("FormatID")]
        public string Format { get; set; }
        [JsonProperty("File_Path")]
        public string FilePath { get; set; }
        [JsonProperty("Archive_Path")]
        public string ArchivePath { get; set; }
        [JsonProperty("File_Pattern")]
        public string FilePattern { get; set; }
        [JsonProperty("Sequence")]
        public int Sequence { get; set; }
        [JsonProperty("Required")]
        public bool Required { get; set; }
        [JsonProperty("LoaderID")]
        public string LoaderID { get; set; }
    }
}

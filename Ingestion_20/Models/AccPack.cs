using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Ingestion_20.API;

namespace Ingestion_20.Models
{
    [Serializable]
    public class AccPack
    {
        [JsonProperty("batchid")]
        public Int64 batchId { get; set; }
        [JsonProperty("id")]
        public Guid? AccPackUid { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("File_Uid")]
        public Guid File_Uid { get; set; }

        [JsonProperty("isnewaccount")]
        public bool isnewaccount { get; set; }

        [JsonProperty("datatype")]
        public string Datatype { get; set; }

        [JsonProperty("custodianUid")]
        public Guid CustodianUid { get; set; }

        [JsonProperty("datasourceUid")]
        public Guid DataSourceGuid { get; set; }

        [JsonProperty("accountUid")]
        public Guid? AccountUID { get; set; }

        [JsonProperty("state")]
        public AccPackState State { get; set; }

        [JsonProperty("statementdate")]
        public DateTime StatementDate { get; set; }

        [JsonProperty("firmid")]
        public Int64? firmId { get; set; }

        [JsonProperty("external")]
        public List<ExternalSecurity> External { get; set; }

        [JsonProperty("internal")]
        public List<Security> InternalNode { get; set; }


        /// <summary>
        /// Persist internal to database
        /// </summary>
        public void Persist()
        {

        }

        /// <summary>
        /// Process to turn external into internal
        /// </summary>
        public async Task Process()
        {
            Mapper mapper = new Mapper();
            
            mapper.Parameters = BuildContext("Transactions");
            if(mapper.Input == null)
            {
                mapper.Input = new List<ExpandoObject>();
            }
            foreach (ExternalSecurity sec in External)
            {              
                foreach (ExpandoObject tran in sec.transactions)
                {
                    mapper.Input.Add(tran);
                }                    
            }

            //these will be loaded from azure function upon accpack load
            mapper.Lookups = GetLookup();
            mapper.Rules = await GetRules(); 
            string mapped = await SendToMapper(mapper);
            mapper = JsonConvert.DeserializeObject<Mapper>(mapped);
        }

        /// <summary>
        /// Persist changes to AccPack database
        /// </summary>
        public void Update()
        {

        }
        /// <summary>
        /// used to build context for mapper calls
        /// </summary>
        private Parameters BuildContext(string dataset)
        {
            Parameters context = new Parameters() { BusinessDate = StatementDate.ToString("yyyy-MM-dd"), Firm = firmId.ToString(), Dataset = dataset };
            return context;
        }

        /// <summary>
        /// used to build input from external data
        /// Raw data will come in the accpack as a key:value pair
        /// </summary>
        private dynamic BuildInput(string rawdata)
        {
            //input properties will be read from the source meta data
            //rawdata index values will also come from the source meta data
            dynamic input = new ExpandoObject();
            foreach(string item in rawdata.Split('|'))
            {
                AddProperty(input, item.Split(':')[0], item.Split(':')[1]);
            }
            input.ProcessId = Guid.NewGuid().ToString();
            return input;
        }

        private async Task<Rules> GetRules()
        {
            return JsonConvert.DeserializeObject<Rules>(await MapperAPI.GetMapperRules("FDAF5238-74D7-4C24-9811-A4CD8C4B1CA8"));
        }
        private List<Lookup> GetLookup()
        {
            ExpandoObject tcodes = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(MapperAPI.GetLookups());
            List<Lookup> lookups = new List<Lookup>();
            lookups.Add(new Lookup { Name = "Tcodes", Values = tcodes });
            return lookups;
        }

        private async Task<string> SendToMapper(Mapper mapper)
        {
            string input = (JsonConvert.SerializeObject(mapper));
            return await MapperAPI.SendToMapper(input);
        }
        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
    }
}

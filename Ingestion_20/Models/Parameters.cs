using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ingestion_20.Models
{
    public class Parameters
    {
        [JsonProperty("Firm")]
        public string Firm { get; set; }
        [JsonProperty("BusinessDate")]
        public string BusinessDate { get; set; }
        [JsonProperty("Dataset")]
        public string Dataset { get; set; }
    }

    public class Mapper
    {
        public Parameters Parameters = new Parameters();
        public List<ExpandoObject> Input = new List<ExpandoObject>();
        public Rules Rules = new Rules();
        public List<Outdata> Workspace = new List<Outdata>();
        public List<Outdata> Output = new List<Outdata>();
        public List<Lookup> Lookups = new List<Lookup>();
        public List<LogMessage> Messages = new List<LogMessage>();
    }
    public class LogMessage
    {
        public const string Category = "Mapping";

        public string Dataset { get; set; }
        public Guid SequenceUID { get; set; }

        public string SequenceName { get; set; }

        //public string SequenceType { get; set; }

        public Guid StepUID { get; set; }

        public string StepName { get; set; }

        public Guid ProcessUID { get; set; }

        public int Row { get; set; }

        public string Type { get; set; }

        public string Message { get; set; }

        public DateTime LogDate { get; set; }
    }
    public class Lookup
    {
        public string Name { get; set; }

        public ExpandoObject Values { get; set; }

    }
    public class Outdata
    {
        public string Entity { get; set; }

        public Guid ProcessId { get; set; }

        //public int WarningCount { get; set; }

        //public int ErrorCount { get; set; }

        //public List<Message> Warnings { get; set; }

        //public List<Message> Errors { get; set; }
        public ExpandoObject Row { get; set; }
    }
    public class Rules
    {
        //public Items Items { get; set; }

        //public List<Attribute> Attributes { get; set; }

        public List<Sequence> Sequences { get; set; }
    }
    public class Sequence
    {
        public Guid SequenceUID { get; set; }
        public string Name { get; set; }
        public List<Step> Steps { get; set; }

    }
    public class Step
    {
        public Guid StepUID { get; set; }
        public string Method { get; set; }
        public string Object { get; set; }
        public string Attribute { get; set; }
        public string Setting { get; set; }
        public string Entity { get; set; }
        public string Comments { get; set; }
        public string LookUp { get; set; }
        public dynamic Default { get; set; }
        public string Message { get; set; }

        public List<dynamic> Params { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Ingestion_20.Models
{
    public class Formatter
    {
        public Guid File_UID { get; set; }
        public Guid Source_UID { get; set; }

        public List<ExpandoObject> DataMapping { get; set; }
    }
}

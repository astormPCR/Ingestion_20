using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader.FileHandling
{
    public class Column
    {
        public string name { get; set; }
        public int length { get; set; }
        public bool tokenize { get; set; }
        public bool primary { get; set; }
        public bool pii { get; set; }
        public bool trimZeros { get; set; }
        public bool checkValue { get; set; }
        public bool ignore { get; set; }
    }
}

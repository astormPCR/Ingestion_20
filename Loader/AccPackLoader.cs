using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader
{
    public class AccPackLoader : ILoader
    {
        public Guid File_UID { get; set; }
        public Guid Source_UID { get; set; }
        public string LoaderType { get; set; }
    }
}

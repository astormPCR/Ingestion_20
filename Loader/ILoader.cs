using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader
{
    public interface ILoader
    {
        Guid File_UID { get; set; }
        Guid Source_UID { get; set; }
        string LoaderType { get; set; }
    }
}

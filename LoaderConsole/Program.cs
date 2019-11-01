using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loader;

namespace LoaderConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            ///args[0] should be the source guid
            int i = ProcessFiles(Guid.Parse(args[0])).Result;
        }

        private static async Task<int> ProcessFiles(Guid sourceUid)
        {
            FileLoader fileLoad = new FileLoader();
            fileLoad.Source_UID = sourceUid;
            await fileLoad.LoadSourceFileConfiguration();
            await fileLoad.CreateClasStructureForFileHelpers();

            return 1;
        }
    }
}

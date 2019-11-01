using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Loader.FileHandling
{
    public interface IFeedFile
    {
        string Name { get; set; }
        string filePath { get; set; }
        string outputPath { get; set; }
        int FeedId { get; set; }
        bool hasPII { get; set; }
        int HeaderRowCount { get; set; }
        int FooterRowCount { get; set; }
        bool isFixed { get; }
        List<Column> Columns { get; set; }
        string currentFileType { get; set; }
        /// <summary>
        /// used only for Excel files
        /// </summary>
        string TemplateFile { get; set; }
        DataTable records { get; set; }
        string  xmlConfiguration { get; set; }
        string headerRows { get; set; }
        string footerRows { get; set; }
        string delimiter { get; set; }
        string ClassDefinition();
        void ProcessCurrentFile(string classDef);
    }
}

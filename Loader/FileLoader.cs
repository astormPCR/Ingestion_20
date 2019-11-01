using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MasterAgg.Models;
using MasterAgg.API;
using Newtonsoft.Json;
using Loader.FileHandling;
using System.Data;
namespace Loader
{
    public class FileLoader : ILoader
    {
        public string LoaderType { get; set; }
        public Guid Source_UID { get; set; }
        public List<SourceFileConfiguration> sourceFiles { get; set; }
        public Guid File_UID { get; set; }
        public Dictionary<Guid, List<SourceFileDefinition>> fileInformation { get; set; }
        public Dictionary<string, string> ClassDefinitions { get; set; }
        public Dictionary<string,DataSet> RawFileRecords { get; set; }
        public FileLoader()
        {
            LoaderType = "File";
            sourceFiles = new List<SourceFileConfiguration>();
            fileInformation = new Dictionary<Guid, List<SourceFileDefinition>>();
            ClassDefinitions = new Dictionary<string, string>();
        }
        /// <summary>
        /// create the class that file helpers uses to read the full file.
        /// </summary>
        public async Task CreateClasStructureForFileHelpers()
        {
            foreach(SourceFileConfiguration file in sourceFiles)
            {
                fileInformation.Add(file.File_UID, await LoadFileDefinition(file.File_UID));
                //Build class for file helpers for current file
                StringBuilder clsdefintion = new StringBuilder();
                clsdefintion.AppendLine(" ");
                clsdefintion.Append("[");
                switch (file.Format)
                {
                    case "CSV":
                        clsdefintion.Append(@"DelimitedRecord("",""),IgnoreFirst(");
                        break;
                    default:
                        break;
                }
                clsdefintion.Append(file.HeaderRowCount);
                clsdefintion.Append("), IgnoreLast(");
                clsdefintion.Append(file.FooterRowCount);
                clsdefintion.Append("), IgnoreEmptyLines");
                clsdefintion.Append("]");
                clsdefintion.AppendLine("");
                clsdefintion.AppendLine("public class  " + file.Name + "{");
                int count = 0;
                foreach(SourceFileDefinition item in fileInformation[file.File_UID])
                {
                    clsdefintion.AppendLine("");
                    string colname;
                    if (item.pii == true) { colname = "pcrtoken" + count.ToString(); } else { colname = item.ColumnName; };
                    clsdefintion.AppendLine("[FieldQuoted('\"', QuoteMode.OptionalForBoth, MultilineMode.AllowForBoth)]");
                    clsdefintion.AppendLine($"public {item.datatype} {colname};");
                    count++;
                }
                clsdefintion.AppendLine("");
                clsdefintion.AppendLine("");
                clsdefintion.AppendLine("");
                clsdefintion.Append(" }");

                ClassDefinitions.Add(file.Name, clsdefintion.ToString());
            }
        }

        public async Task LoadSourceFileConfiguration()
        {
            SequencerAPI sapi = new SequencerAPI();
            string sourcefileJson = await sapi.GetFiles(Source_UID);
            sourceFiles.Add(JsonConvert.DeserializeObject<SourceFileConfiguration>(sourcefileJson));
        }
        public async Task<List<SourceFileDefinition>> LoadFileDefinition(Guid fileUid)
        {
            SequencerAPI sapi = new SequencerAPI();
            string sourcedefintionJson = await sapi.GetFileDefinition(fileUid);
            return JsonConvert.DeserializeObject<List<SourceFileDefinition>>(sourcedefintionJson);
        }
        /// <summary>
        /// uses the sourcefile information to grab each file within referenced directory and 
        /// loads into the rawFilesRecord Dictionary for each file type.
        /// </summary> 
        public void ReadAllFiles()
        {
            foreach(SourceFileConfiguration file in sourceFiles)
            {
                //grab path and file map
                if (Directory.Exists(file.FilePath))
                {
                    
                }
                else
                {
                    ///create workflow with High importance
                }
            }
        }
    }
}

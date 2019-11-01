using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FileHelpers;
using System.Threading.Tasks;

namespace Loader.FileHandling
{
    public class DelimitedFeedFile : IFeedFile
    {
        #region "Interface Properties"
        public bool isFixed { get; set; }
        public string delimiter { get; set; }
        public List<Column> Columns { get; set; }
        public int FeedId { get; set; }
        public string filePath { get; set; }
        public int FooterRowCount { get; set; }
        public string footerRows { get; set; }
        public bool hasPII { get; set; }
        public int HeaderRowCount { get; set; }
        public string headerRows { get; set; }
        public string Name { get; set; }
        public string outputPath { get; set; }
        public string xmlConfiguration { get; set; }
        public int MaxColumnCount { get; set; }
        public string ClassDefinition()
        {

            StringBuilder clsdefintion = new StringBuilder();

            //clsdefintion.AppendLine(" ");
            //clsdefintion.Append("[");
            //clsdefintion.Append(@"DelimitedRecord(""" + this.delimiter + @"""),IgnoreFirst(");
            //clsdefintion.Append(_headerRowCount);
            //clsdefintion.Append("), IgnoreLast(");
            //clsdefintion.Append(_footerRowCount);
            //clsdefintion.Append("), IgnoreEmptyLines");
            //clsdefintion.Append("]");

            clsdefintion.AppendLine(" ");

            //clsdefintion.AppendLine("public class  " + this._name + "{");
            int count = 0;
            foreach (Column col in this.Columns)
            {
                clsdefintion.AppendLine("");
                string colname;
                if (col.tokenize == true) { colname = "pcrtoken" + count.ToString(); } else { colname = col.name; };
                clsdefintion.AppendLine("[FieldQuoted('\"', QuoteMode.OptionalForBoth, MultilineMode.AllowForBoth)]");
                clsdefintion.AppendLine("public string " + colname + ";");
                count++;
            }

            clsdefintion.AppendLine("");
            clsdefintion.AppendLine("");
            clsdefintion.AppendLine("");
            clsdefintion.Append(" }");

            return clsdefintion.ToString();
        }
        #endregion
        public DataTable records { get; set; }
        public bool isAnonymizedFile { get; set; }
        public string currentFileType { get; set; }
        public string TemplateFile { get; set; }
        //private Dictionary<string, string> _feedConfig;
        public DelimitedFeedFile()
        {
            //_feedConfig = feedConfig;
        }
        public DelimitedFeedFile(string xmlConfig)
        {
            //_xmlDefinition = xmlConfig;
            //_columns = new List<Column>();
        }

        virtual public void ProcessCurrentFile(string clsFileIn)
        {
            Type feedin = FileHelpers.Dynamic.ClassBuilder.ClassFromString(clsFileIn);
            try
            {
                FileHelperEngine engineIn = new FileHelperEngine(feedin);
                engineIn.BeforeReadRecord += (eng, e) =>
                {
                    if (e.RecordLine.Length == 0)
                        e.SkipThisRecord = true;
                    else
                    {
                        if (delimiter == ",")
                        {
                            e.RecordLine = e.RecordLine.Replace("\",\"", "\"|\"");
                            e.RecordLine = e.RecordLine.Replace(",", " ");
                            e.RecordLine = e.RecordLine.Replace("|", ",");
                            e.RecordLine = e.RecordLine.Replace("\",\"", "^,^");
                            e.RecordLine = e.RecordLine.Replace("\"", "");
                            e.RecordLine = e.RecordLine.Replace("^", "\"");
                            e.RecordLine = $"\"{e.RecordLine.Trim()}\"";
                        }
                    }
                };

                var dRecords = engineIn.ReadFile(filePath);
                //records = DataTableExtensions.ToDataTable<Object>(dRecords.ToList<object>());
                //FileProcess fp = new FileProcess(this);
               // fp.Process();
            }
            catch (BadUsageException badFormat)
            {
                // Logging.Log.Instance.Error(badFormat, "Error, File Helpers Bad Format in dynamic class creation: {0}", badFormat);
            }
            catch (Exception ex)
            {
                // Logging.Log.Instance.Fatal(ex, "Fatal System Error: {0}", ex);
            }
        }
    }
}

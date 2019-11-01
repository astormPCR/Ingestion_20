using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader.FileHandling
{
    public class Feed
    {
        //public DataTable records { get; set; }
        //int _feedId;
        //string _fileInfo;
        //bool hasPII = false;
        //string _parentFeedType;
        //public Dictionary<string, List<string>> fileNameList;
        //processType currentProcess;
        //XDocument transformedXML;
        //string xmlConfigPath;
        //List<Column> feedColumns;
        //List<string> _tokenColumnNames;
        //Feedconfiguration _feedConfig;
        //string classDef;

        //public Feed(int feedID, XDocument configurationXML, processType pType, string parentFeedType, Feedconfiguration feedConfig)
        //{
        //    _feedId = feedID;
        //    _fileInfo = feedConfig.filetypes.file.Find(x => x.name == parentFeedType).location;
        //    _parentFeedType = parentFeedType;
        //    transformedXML = configurationXML;
        //    feedColumns = new List<Column>();
        //    fileNameList = new Dictionary<string, List<string>>();
        //    currentProcess = pType;
        //    xmlConfigPath = feedConfig.filetypes.file.Find(x => x.name == parentFeedType).configuration;
        //    _feedConfig = feedConfig;
        //    _tokenColumnNames = new List<string>();
        //    // Logging.Log.Instance.Log(NLog.LogLevel.Info, String.Format("Begin File Creation"));
        //    TransformXML();
        //    //  Logging.Log.Instance.Log(NLog.LogLevel.Trace, string.Format("Completed Transformation begin running all files: {0}", NLog.Time.AccurateLocalTimeSource.Current.Time.ToString()));
        //    RunFile();
        //}
        //public void TransformXML()
        //{
        //    string fileType = transformedXML.Root.Element("filedef").Attribute("type").Value;

        //    if (fileType.ToLower() == "fixed")
        //    {
        //        TransformFixedXML(ref transformedXML);
        //    }
        //    else
        //    {

        //        TransformDelimitedXML(ref transformedXML);
        //    }

        //    CreateColumnsFromXML(transformedXML);

        //    if (fileType.ToLower() == "fixed")
        //    {
        //        classDef = FixedClassDefinition(transformedXML);
        //    }
        //    else
        //    {
        //        classDef = DelimitedClassDefinition(transformedXML);
        //    }

        //}
        //private void TransformDelimitedXML(ref XDocument _xmlDefinition)
        //{

        //    SortedList<int, XElement> finalList = new SortedList<int, XElement>();
        //    if (_xmlDefinition.Root.Element("newfields") == null)
        //        _xmlDefinition.Root.Add(new XElement("newfields"));
        //    XElement lastNode;
        //    try { lastNode = (XElement)_xmlDefinition.Root.Element("fields").DescendantNodes().Last(); }
        //    catch { lastNode = (XElement)_xmlDefinition.Root.Element("newfields").DescendantNodes().Last(); }
        //    int countXml = int.Parse(lastNode.Attribute("fieldid").Value.ToString());


        //    // Begin the column compare process
        //    int finalColCount = 0;
        //    // Compare the file column count to the xml column count
        //    if (GetFileColumnCount().Any(x => x > (countXml + 1)))
        //    {
        //        //     Logging.Log.Instance.Log(NLog.LogLevel.Info, String.Format("Xml Last Field ID less than File Column Count"));
        //        // if last field ID + 1 is less than the file count, use the file count and insert place holders
        //        finalColCount = (GetFileColumnCount().Max() - 1);
        //    }
        //    else
        //    {
        //        //      Logging.Log.Instance.Log(NLog.LogLevel.Info, String.Format("Xml Last Field ID equal or greater than File Column Count"));
        //        // Check passed, keep the xml last field ID as the count
        //        finalColCount = countXml;
        //    }

        //    for (var i = 0; i <= countXml; i++)
        //    {
        //        if (_xmlDefinition.XPathSelectElement("descendant::field[@fieldid='" + i.ToString() + "']") == null)
        //        {
        //            //node does not exist with the current id so create dummy node.
        //            XElement newNode = new XElement("field");
        //            newNode.Add(new XAttribute("name", "placeholder" + i.ToString()));
        //            newNode.Add(new XAttribute("start", 0));
        //            newNode.Add(new XAttribute("length", 0));
        //            newNode.Add(new XAttribute("fieldid", i));
        //            finalList.Add(i, newNode);
        //        }
        //        else
        //        {
        //            XElement existingNode = _xmlDefinition.XPathSelectElement("descendant::field[@fieldid='" + i.ToString() + "']");
        //            finalList.Add(i, existingNode);
        //        }
        //    }
        //    try
        //    {
        //        _xmlDefinition.Root.Element("fields").Remove();
        //    }
        //    catch { }

        //    foreach (int i in finalList.Keys)
        //    {
        //        string check = finalList[i].Attribute("name").Value.Trim();
        //        finalList[i].Attribute("name").SetValue(check.Replace(" ", "_").Replace("(", "").Replace(")", "").Replace("/", "").Replace("\\", "").Replace("?", "").Replace("'", "").Replace("-", ""));
        //        _xmlDefinition.Root.Element("newfields").Add(finalList[i]);
        //    }
        //}
        //private void TransformFixedXML(ref XDocument _xmlDefinition)
        //{
        //    List<int> nodestoProcess = GetPIIFields(_xmlDefinition);

        //    int firstPIINode = nodestoProcess[0];
        //    int lastPIINode = nodestoProcess[nodestoProcess.Count - 1];

        //    if (firstPIINode == lastPIINode)
        //    {
        //        //single pii node. 
        //        //Logging.Log.Instance.Log(NLog.LogLevel.Warn, String.Format("Single PII Node {0}:", _xmlDefinition));
        //        var firstnode = _xmlDefinition.XPathSelectElement("descendant::field[@fieldid='" + firstPIINode + "']");
        //        if (firstPIINode == 0)
        //        {
        //            if (_xmlDefinition.Element("newfields") == null)
        //                _xmlDefinition.Root.Add(new XElement("newfields"));
        //            _xmlDefinition.Root.Element("newfields").Add(firstnode);
        //            int startlength = int.Parse(firstnode.Attribute("length").Value) + 1;
        //            XElement finalNode = (XElement)_xmlDefinition.Root.Element("fields").LastNode;
        //            int endlength = int.Parse(finalNode.Attribute("length").Value) + int.Parse(finalNode.Attribute("start").Value);
        //            XElement newNode = new XElement("field");
        //            newNode.Add(new XAttribute("name", "other"));
        //            newNode.Add(new XAttribute("start", startlength));
        //            newNode.Add(new XAttribute("length", startlength + endlength));
        //            newNode.Add(new XAttribute("fieldid", 1));
        //            _xmlDefinition.Root.Element("newfields").Add(newNode);
        //        }
        //        else
        //        {
        //            XElement newNode = new XElement("field");
        //            newNode.Add(new XAttribute("name", "placeholder1"));
        //            newNode.Add(new XAttribute("start", 0));
        //            int p1 = int.Parse(((XElement)_xmlDefinition.Root.Element("fields").FirstNode).Attribute("start").Value) + int.Parse(((XElement)_xmlDefinition.Root.Element("fields").FirstNode).Attribute("length").Value);
        //            int p2 = int.Parse(firstnode.Attribute("start").Value) + int.Parse(firstnode.Attribute("length").Value);
        //            newNode.Add(new XAttribute("length", p1));
        //            if (_xmlDefinition.Element("newfields") == null)
        //                _xmlDefinition.Root.Add(new XElement("newfields"));
        //            _xmlDefinition.Root.Element("newfields").Add(newNode);
        //            _xmlDefinition.Root.Element("newfields").Add(firstnode);
        //            XElement lastNode = new XElement("field");
        //            lastNode.Add(new XAttribute("name", "placeholder2"));
        //            lastNode.Add(new XAttribute("start", p2));
        //            int p3 = int.Parse(((XElement)_xmlDefinition.Root.Element("fields").LastNode).Attribute("start").Value) + int.Parse(((XElement)_xmlDefinition.Root.Element("fields").LastNode).Attribute("length").Value);
        //            lastNode.Add(new XAttribute("length", p3));
        //            _xmlDefinition.Root.Element("newfields").Add(lastNode);
        //        }
        //    }
        //    else
        //    {
        //        if (_xmlDefinition.Element("newfields") == null)
        //            _xmlDefinition.Root.Add(new XElement("newfields"));

        //        for (int i = 0; i < (nodestoProcess.Count); i++)
        //        {
        //            if (nodestoProcess[i] == 0)
        //            {
        //                //first token node is the first one. add this to the newfields node
        //                //  Logging.Log.Instance.Log(NLog.LogLevel.Trace, String.Format("First token node is the first node"));
        //                _xmlDefinition.Root.Element("newfields").Add(_xmlDefinition.Root.Element("fields").Elements("field").Where(field => field.Attribute("fieldid").Value == i.ToString()).First());
        //            }
        //            else
        //            {
        //                if (i == 0)
        //                {
        //                    //first is special case as we need to start at the fieldid = 0 and get the end to create a new node.
        //                    XElement node = new XElement("field");
        //                    node.Add(new XAttribute("start", 0)); //will always start at 0 if first node.
        //                    XElement cNode = _xmlDefinition.Root.Element("fields").Elements("field").Where(field => field.Attribute("fieldid").Value == nodestoProcess[i].ToString()).First();
        //                    int length = int.Parse(cNode.Attribute("start").Value);
        //                    node.Add(new XAttribute("length", length));
        //                    node.Add(new XAttribute("name", "placeholder" + nodestoProcess[i].ToString()));
        //                    _xmlDefinition.Root.Element("newfields").Add(node);//add first new node
        //                    //then add token/pii node
        //                    _xmlDefinition.Root.Element("newfields").Add(cNode);
        //                }
        //                else
        //                {

        //                    XElement node = new XElement("field");
        //                    XElement cNode = _xmlDefinition.Root.Element("fields").Elements("field").Where(field => field.Attribute("fieldid").Value == nodestoProcess[i].ToString()).First();
        //                    XElement pNode = _xmlDefinition.Root.Element("fields").Elements("field").Where(field => field.Attribute("fieldid").Value == nodestoProcess[i - 1].ToString()).First();
        //                    if (Math.Abs(nodestoProcess[i] - nodestoProcess[i - 1]) == 1)
        //                    {
        //                        //nodes are together and no non-pii node between.
        //                        _xmlDefinition.Root.Element("newfields").Add(cNode);
        //                    }
        //                    else
        //                    {
        //                        int start = int.Parse(pNode.Attribute("length").Value) + int.Parse(pNode.Attribute("start").Value);
        //                        node.Add(new XAttribute("start", start));
        //                        int length = Math.Abs(start - int.Parse(cNode.Attribute("start").Value));
        //                        node.Add(new XAttribute("length", length));
        //                        node.Add(new XAttribute("name", "placeholder" + nodestoProcess[i].ToString()));
        //                        _xmlDefinition.Root.Element("newfields").Add(node);
        //                        _xmlDefinition.Root.Element("newfields").Add(cNode);
        //                    }
        //                    if (nodestoProcess.Count - 1 == i)
        //                    {
        //                        //is this the last tokenized node? if yes, is it the last node in the file?
        //                        if (nodestoProcess[i] < _xmlDefinition.Root.Element("fields").Elements().Count())
        //                        {
        //                            XElement finalnode = new XElement("field");
        //                            int fstart = int.Parse(cNode.Attribute("length").Value) + int.Parse(cNode.Attribute("start").Value);
        //                            finalnode.Add(new XAttribute("start", fstart));
        //                            int flength = Math.Abs(fstart - int.Parse(((XElement)_xmlDefinition.Root.Element("fields").LastNode).Attribute("start").Value)) + int.Parse(((XElement)_xmlDefinition.Root.Element("fields").LastNode).Attribute("length").Value);
        //                            finalnode.Add(new XAttribute("length", flength));
        //                            finalnode.Add(new XAttribute("name", "placeholder"));
        //                            _xmlDefinition.Root.Element("newfields").Add(finalnode);
        //                            //  Logging.Log.Instance.Log(NLog.LogLevel.Trace, String.Format("Last tokenized node, and last node in file: {0}", finalnode));
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    _xmlDefinition.Root.Element("fields").Remove();

        //    foreach (XElement item in _xmlDefinition.Root.Element("newfields").Descendants())
        //    {
        //        string check = item.Attribute("name").Value;
        //        item.Attribute("name").SetValue(check.Replace(" ", "_").Replace("(", "").Replace(")", "").Replace("/", "").Replace("\\", "").Replace("?", "").Replace("'", "").Replace("-", ""));
        //    }

        //}
        //private void CreateColumnsFromXML(XDocument _xmlDefinition)
        //{
        //    foreach (XElement col in _xmlDefinition.Element("filemap").Element("newfields").Elements().ToList())
        //    {
        //        Column newCOL = new Column();
        //        newCOL.name = col.Attribute("name").Value.Replace("[", "").Replace("]", "");
        //        if (col.Attribute("tokenize") != null)
        //            newCOL.tokenize = col.Attribute("tokenize").Value == "true" ? true : false;
        //        else
        //            newCOL.tokenize = false;

        //        if (col.Attribute("primary") != null)
        //            newCOL.primary = col.Attribute("primary").Value == "true" ? true : false;
        //        else
        //            newCOL.primary = false;
        //        if (col.Attribute("secondCheck") != null)
        //        {
        //            newCOL.checkValue = true;
        //        }
        //        else
        //        {
        //            newCOL.checkValue = false;
        //        }
        //        if (col.Attribute("pii") != null)
        //        {
        //            newCOL.pii = col.Attribute("pii").Value == "true" ? true : false;
        //            hasPII = true;
        //        }
        //        else
        //            newCOL.pii = false;
        //        if (col.Attribute("ignore") != null)
        //        {
        //            newCOL.ignore = col.Attribute("ignore").Value == "true" ? true : false;
        //        }
        //        else
        //            newCOL.ignore = false;


        //        newCOL.length = int.Parse(col.Attribute("length").Value);
        //        if (col.Attribute("trimzeros") != null)
        //            newCOL.trimZeros = true;
        //        else
        //            newCOL.trimZeros = false;
        //        if (feedColumns.Find(x => x.name == newCOL.name) != null)
        //        {
        //            newCOL.name = newCOL.name + "1";
        //            feedColumns.Add(newCOL);
        //        }
        //        else
        //            feedColumns.Add(newCOL);
        //    }
        //}
        //private List<int> GetPIIFields(XDocument _xmlDefinition)
        //{
        //    List<int> valuesForProcessing = new List<int>();
        //    foreach (XElement item in _xmlDefinition.Root.Element("fields").Elements())
        //    {
        //        if (item.Attribute("tokenize") != null)
        //        {
        //            valuesForProcessing.Add(int.Parse(item.Attribute("fieldid").Value));
        //            _tokenColumnNames.Add(item.Attribute("name").Value);
        //        }
        //    }
        //    return valuesForProcessing;
        //}
        //private string DelimitedClassDefinition(XDocument transformedXML)
        //{
        //    string delimiter = transformedXML.Root.Element("filedef").Attribute("delimiter").Value;
        //    if (delimiter == " ")
        //    {
        //        delimiter = @"\t";
        //    }
        //    string _headerRowCount = transformedXML.Root.Element("filedef").Attribute("numberheaderrows").Value;
        //    string _footerRowCount = transformedXML.Root.Element("filedef").Attribute("numberfooterrows").Value;
        //    StringBuilder clsdefintion = new StringBuilder();
        //    clsdefintion.AppendLine("using System.Text.RegularExpressions;");
        //    clsdefintion.AppendLine(" ");
        //    clsdefintion.Append("[");
        //    clsdefintion.Append(@"DelimitedRecord(""" + delimiter + @"""),IgnoreFirst(");
        //    clsdefintion.Append(_headerRowCount);
        //    clsdefintion.Append("), IgnoreLast(");
        //    clsdefintion.Append(_footerRowCount);
        //    clsdefintion.Append("), IgnoreEmptyLines");
        //    clsdefintion.Append("]");

        //    clsdefintion.AppendLine(" ");

        //    clsdefintion.AppendLine("public class  " + _parentFeedType + "{");
        //    int count = 0;
        //    foreach (Column col in feedColumns)
        //    {
        //        clsdefintion.AppendLine("");
        //        string colname;
        //        colname = col.name;
        //        //if (col.tokenize == true) { colname = "pcrtoken" + count.ToString(); } else { colname = col.name; };


        //        if (colname.Contains("Description"))
        //        {
        //            // clsdefintion.AppendLine("[FieldConverter(typeof(DescriptionQuotesConverter))]");

        //            clsdefintion.AppendLine("[FieldTrim(TrimMode.Both) ]");
        //            clsdefintion.AppendLine("[FieldQuoted('\"',QuoteMode.OptionalForBoth,MultilineMode.AllowForBoth)]");
        //        }
        //        else
        //        {
        //            clsdefintion.AppendLine("[FieldQuoted('\"',QuoteMode.OptionalForBoth,MultilineMode.AllowForBoth)]");
        //            clsdefintion.AppendLine("[FieldTrim(TrimMode.Both)]");
        //        }
        //        if (col.ignore)
        //        {
        //            clsdefintion.AppendLine("[FieldValueDiscarded]");
        //            //clsdefintion.AppendLine("[FieldOptional]");
        //        }
        //        else
        //        {
        //            //clsdefintion.AppendLine("[FieldOptional]");
        //        }


        //        clsdefintion.AppendLine("public string " + colname + ";");
        //        count++;
        //    }

        //    clsdefintion.AppendLine("");
        //    clsdefintion.AppendLine("");
        //    clsdefintion.AppendLine("");
        //    clsdefintion.Append(" }");
        //    clsdefintion.AppendLine("");
        //    clsdefintion.AppendLine("");
        //    ///class to convert field


        //    clsdefintion.AppendLine(" public class DescriptionQuotesConverter : ConverterBase  {");
        //    clsdefintion.AppendLine(" public override object StringToField(string from)  {");
        //    clsdefintion.AppendLine(" from.Replace(\",\", \" \");");
        //    clsdefintion.AppendLine(" from = Regex.Replace(from, @\"([^^, rn])\"\"([^$, rn])\", @\"$1$2\"); "); // from.Replace('\"', ' ');  }");
        //    clsdefintion.AppendLine(" return from; } } ");
        //    clsdefintion.AppendLine("");
        //    clsdefintion.AppendLine("");

        //    //  Logging.Log.Instance.Log(NLog.LogLevel.Trace, String.Format("Delimited Class Definition : {0}", clsdefintion.ToString()));

        //    return clsdefintion.ToString();
        //}
        //private string FixedClassDefinition(XDocument transformedXML)
        //{
        //    string _headerRowCount = transformedXML.Root.Element("filedef").Attribute("numberheaderrows").Value;
        //    string _footerRowCount = transformedXML.Root.Element("filedef").Attribute("numberfooterrows").Value;

        //    StringBuilder clsDefinition = new StringBuilder();
        //    clsDefinition.AppendLine(" ");

        //    clsDefinition.Append("[");
        //    clsDefinition.Append("FixedLengthRecord((FixedMode.AllowVariableLength)),IgnoreFirst(");
        //    clsDefinition.Append(_headerRowCount);
        //    clsDefinition.Append("), IgnoreLast(");
        //    clsDefinition.Append(_footerRowCount);
        //    clsDefinition.Append(")");
        //    clsDefinition.Append("]");

        //    clsDefinition.AppendLine(" ");

        //    clsDefinition.AppendLine("public class  " + _parentFeedType + "{");
        //    int count = 0;
        //    foreach (Column col in feedColumns)
        //    {
        //        clsDefinition.AppendLine("");
        //        string colname;
        //        if (col.tokenize == true) { colname = "pcrtoken" + count.ToString(); } else { colname = col.name; };
        //        clsDefinition.AppendLine("[FieldFixedLength(" + col.length.ToString() + ")]");

        //        clsDefinition.AppendLine("public string " + colname + ";");
        //        count++;
        //    }

        //    clsDefinition.AppendLine("");

        //    clsDefinition.AppendLine("");
        //    clsDefinition.AppendLine("");
        //    clsDefinition.Append(" }");

        //    // Logging.Log.Instance.Log(NLog.LogLevel.Trace, String.Format("Fixed Class Definition : {0}", clsDefinition.ToString()));

        //    return clsDefinition.ToString();
        //}
        //private feedFileType currentFileType()
        //{
        //    string fType = transformedXML.Root.Element("filedef").Attribute("type").Value;

        //    if (fType.ToLower() == "fixed")
        //        return feedFileType.Fixed;
        //    else if (fType.ToLower() == "delimited")
        //        return feedFileType.Delimited;
        //    else
        //        return feedFileType.Excel;
        //}

        //private List<int> GetFileColumnCount()
        //{
        //    var delimiter = transformedXML.Root.Element("filedef").Attribute("delimiter").Value;
        //    //var headerText = headerRows;
        //    List<int> colCount = new List<int>();
        //    string filePath = _feedConfig.filetypes.file.Find(x => x.name == _parentFeedType).location;
        //    string fileMask = _feedConfig.filetypes.file.Find(x => x.name == _parentFeedType).mask;
        //    List<string> fileList = Directory.EnumerateFiles(filePath, fileMask).ToList();
        //    if (fileList.Count() > 0)
        //    {
        //        foreach (string f in fileList)
        //        {
        //            using (var reader = System.IO.File.OpenText($"{f}"))
        //            {
        //                while (!string.IsNullOrWhiteSpace(reader.ReadLine()) && reader.Read() != 0 && !reader.EndOfStream)
        //                {
        //                    if (reader.ReadLine().Split(($"\"{delimiter}\"").ToCharArray(), StringSplitOptions.RemoveEmptyEntries) != null && !reader.EndOfStream)
        //                    {
        //                        colCount.Add(reader.ReadLine().Split(delimiter.ToCharArray()).Length);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // no file for item type/mask - need to create workflow entry
        //    }
        //    return colCount;
        //}
        //public void RunFile()
        //{
        //    feedFileType internalFeedType = currentFileType();
        //    IFeedFile feedFilein;
        //    // Logging.Log.Instance.Log(NLog.LogLevel.Trace, String.Format("Running file type: {0}", internalFeedType));
        //    switch (internalFeedType)
        //    {
        //        case feedFileType.Delimited:
        //            feedFilein = new DelimitedFeedFile(transformedXML);
        //            break;
        //        case feedFileType.Fixed:
        //            feedFilein = new FixedFeedFile(transformedXML);
        //            break;
        //        case feedFileType.Excel:
        //            feedFilein = new ExcelFeedFile(transformedXML);
        //            break;
        //        default:
        //            feedFilein = new FixedFeedFile(transformedXML);
        //            break;
        //    }
        //    feedFilein.Name = _parentFeedType;
        //    feedFilein.currentFileType = internalFeedType;
        //    feedFilein.hasPII = hasPII;
        //    feedFilein.HeaderRowCount = int.Parse(transformedXML.Root.Element("filedef").Attribute("numberheaderrows").Value);
        //    feedFilein.FooterRowCount = int.Parse(transformedXML.Root.Element("filedef").Attribute("numberfooterrows").Value);
        //    feedFilein.FeedId = _feedId;
        //    feedFilein.Columns = feedColumns;
        //    feedFilein.xmlConfiguration = transformedXML;
        //    feedFilein.delimiter = transformedXML.Root.Element("filedef").Attribute("delimiter").Value;
        //    feedFilein.TemplateFile = xmlConfigPath + @"\" + _parentFeedType + "Template.xlsx";
        //    //  Logging.Log.Instance.Log(NLog.LogLevel.Info, String.Format("XML Configfile transformed: {0}", transformedXML.ToString()));
        //    string filePath = _feedConfig.filetypes.file.Find(x => x.name == _parentFeedType).location;
        //    string fileMask = _feedConfig.filetypes.file.Find(x => x.name == _parentFeedType).mask;
        //    List<string> fileList = Directory.EnumerateFiles(filePath, fileMask).ToList();
        //    fileNameList.Add(_parentFeedType, fileList);
        //    if (fileList.Count() > 0)
        //    {
        //        foreach (string f in fileList)
        //        {
        //            feedFilein.filePath = f;
        //            feedFilein.ProcessCurrentFile(classDef);
        //            if (records == null)
        //                records = feedFilein.records;
        //            else
        //                records.Merge(feedFilein.records);
        //        }
        //    }
        //    else
        //    {
        //        //log no file for... workflow items?
        //    }
        //}
    }
}

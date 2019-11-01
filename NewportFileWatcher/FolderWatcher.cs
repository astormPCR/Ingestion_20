using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization; 
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;
using Serilog;
using Topshelf;


namespace FeedHandlerWatcher
{
    public class FolderWatcher
    {
        #region Private variables

        /// <summary>
        /// Keeps a list of all the file watcher listeners, predefined on an XML file
        /// </summary>
        private List<CustomFolderSettings> listFolders;

        /// <summary>
        /// Keeps a list of all the file system watchers in execution.
        /// </summary>
        private List<FileSystemWatcher> listFileSystemWatcher;
        private SourceFileConfiguration sourceFile;
        /// <summary>
        /// Name of the XML file where resides the specification of folders and extensions to be monitored
        /// </summary>
        private string fileNameXML;
        private static ILogger _log;
        #endregion

        public void Start()
        {
            // Initialize the list of filesystem Watchers based on the XML configuration file
            //PopulateListFileSystemWatchers();
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("SourceContext", null)
                .WriteTo.RollingFile(@"c:\FileIngestion\file.log",
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {Message} ({SourceContext:l}){NewLine}{Exception}", shared: true)
                .CreateLogger();
            _log = Log.Logger;
            // Start the file system watcher for each of the file specification and folders found on the List<>
            //First Grab the configuration information from the API
            SequencerAPI sequencerAPI = new SequencerAPI();
            sourceFile = JsonConvert.DeserializeObject<SourceFileConfiguration>(sequencerAPI.GetFiles(Guid.Parse("E4B30A37-D0BF-49D4-BEE9-A0CC19959E6D")));

            StartFileSystemWatcher();
        }
        public void Stop()
        {

        }
        #region Private Methods

        /// <summary>
        /// Stops the main execution of the Windows Service
        /// </summary>
        private void StopMainExecution()
        {
            if (listFileSystemWatcher != null)
            {
                foreach (FileSystemWatcher fsw in listFileSystemWatcher)
                {
                    // Stop listening
                    fsw.EnableRaisingEvents = false;

                    // Record a log entry into Windows Event Log
                    CustomLogEvent(string.Format("Stop monitoring files with extension ({0}) in the folder ({1})", fsw.Filter, fsw.Path));

                    // Dispose the Object
                    fsw.Dispose();
                }

                // Cleans the list
                listFileSystemWatcher.Clear();
            }
        }

        /// <summary>
        /// Start the file system watcher for each of the file specification and folders found on the List<>
        /// </summary>
        private void StartFileSystemWatcher()
        {
            // Creates a new instance of the list
            this.listFileSystemWatcher = new List<FileSystemWatcher>();

            // Loop the list to process each of the folder specifications found
           // foreach (CustomFolderSettings customFolder in listFolders)
           // {
                DirectoryInfo dir = new DirectoryInfo(sourceFile.FilePath);

                // Checks whether the SystemWatcher is enabled and also the directory is a valid location
                if (dir.Exists)
                {
                    // Creates a new instance of FileSystemWatcher
                    FileSystemWatcher fileSWatch = new FileSystemWatcher();

                    // Sets the filter
                    fileSWatch.Filter = sourceFile.FilePattern;

                    // Sets the folder location
                    fileSWatch.Path = sourceFile.FilePath;
                // Sets the action to be executed
                //StringBuilder actionToExecute = new StringBuilder(customFolder.ExecutableFile);

                // List of arguments
                // StringBuilder actionArguments = new StringBuilder(customFolder.ExecutableArguments);
                string actionToExecute = "C:\\FeedHandler\\TestHarness.exe";
                string actionArguments = $"490 {sourceFile.Source_UID.ToString()}";
                    // Subscribe to notify filters
                    fileSWatch.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

                    // Associate the event that will be triggered when a new file is added to the monitored folder, using a lambda Expression                    
                    fileSWatch.Created += (senderObj, fileSysArgs) => fileSWatch_Created(senderObj, fileSysArgs, actionToExecute.ToString(), actionArguments.ToString(), sourceFile.FilePath, sourceFile.ArchivePath, sourceFile.FilePath, 1);

                    // Begin watching
                    fileSWatch.EnableRaisingEvents = true;

                    // Add the systemWatcher to the list
                    listFileSystemWatcher.Add(fileSWatch);

                    // Record a log entry into Windows Event Log
                    CustomLogEvent(string.Format("Starting to monitor files with extension ({0}) in the folder ({1})", fileSWatch.Filter, fileSWatch.Path));
                }
                else
                {
                    // Record a log entry into Windows Event Log
                    //CustomLogEvent(string.Format("File system monitor cannot start because the folder ({0}) does not exist", customFolder.FolderPath));
                }
            //}
        }

        /// <summary>
        /// This event is triggered when a file with the specified extension is created on the monitored folder
        /// </summary>
        /// <param name="sender">Object raising the event</param>
        /// <param name="e">List of arguments - FileSystemEventArgs</param>
        /// <param name="action_Exec">The action_ execute.</param>
        /// <param name="action_Args">The action_ arguments.</param>
        void fileSWatch_Created(object sender, FileSystemEventArgs e, string action_Exec, string action_Args, string processFolder, string archiveFolder, string watchedFolder, int fileCount)
        {
            // Gets the name of the file recently added
            string fileName = e.Name;
            CustomLogEvent($"Trigger file has been created:  {e.Name}");
            //move files and start
            FileManager fm = new FileManager();
            
            Guid source_uid = Guid.Parse(action_Args.Split(' ')[1]);
            DateTime? fileDateTime = null;
            long firmid = long.Parse(action_Args.Split(' ')[0]);
            if ((Directory.EnumerateFiles(watchedFolder).Count() == fileCount))
            {
                //check the dates for all files in the folder to determine if they are all the same.
                bool CanContinueWProcessing = true;  //only look to this when all files are present
                foreach (String fi in Directory.EnumerateFiles(watchedFolder))
                {

                    int milliseconds = int.Parse(ConfigurationManager.AppSettings["TimeDelay"]);
                    Thread.Sleep(milliseconds);
                    CanContinueWProcessing = CheckFile(fi, firmid);
                    FileInfo info = new FileInfo(fi);                   
                    fm.MoveFileforProcessing(info.Name, watchedFolder, processFolder, Guid.Parse(action_Args.Split(' ')[1]));
                //}
                
                //foreach (string fi in Directory.EnumerateFiles(processFolder))
                //{
                    if (fileDateTime == null)
                    {
                        fileDateTime = GetStatementDateFromFilename(fi, source_uid);
                    }
                    else
                    {
                        //this is checking to determine if the date of the next file is the same to use it for processing.
                        if (fileDateTime.Value.Date != GetStatementDateFromFilename(fi, source_uid).Value.Date)
                        {
                            CanContinueWProcessing = false;
                        }
                    }
                }
                // Adds the file name to the arguments.  The filename will be placed in lieu of {0}
                string newStr = string.Format(action_Args, fileName);
                if (CanContinueWProcessing)
                {
                    //fm.MoveFileforProcessing();
                    // Executes the command from the DOS window
                    ExecuteCommandLineProcess(action_Exec, newStr, processFolder, archiveFolder, e.Name);
                }
            }            
        }
        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
        /// <summary>
        /// Record messages and logs into the Windows Event log
        /// </summary>
        /// <param name="message">Message to be recorded into the Windows Event log</param>
        public static void CustomLogEvent(string message)
        {        
            DateTime dt = new DateTime();
            dt = System.DateTime.UtcNow;
            message = dt.ToLocalTime() + ": " + message;
            _log.Information(message);
        }

        /// <summary>
        /// Reads an XML file and populates a list of <CustomFolderSettings>
        /// </summary>
        private void PopulateListFileSystemWatchers()
        {
            /// Get the XML file name from the APP.Config file
            fileNameXML = ConfigurationManager.AppSettings["XMLFileFolderSettings"];

            // Creates an instance of XMLSerializer
            XmlSerializer deserializer = new XmlSerializer(typeof(List<CustomFolderSettings>));

            using (var fileS = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + fileNameXML, FileMode.Open))
            {
                object obj = deserializer.Deserialize(fileS);
                // Obtains a list of strongly typed CustomFolderSettings from XML Input data
                listFolders = obj as List<CustomFolderSettings>;
            }
        }

        /// <summary>
        /// Executes a set of instructions through the command window
        /// </summary>
        /// <param name="executableFile">Name of the executable file or program</param>
        /// <param name="argumentList">List of arguments</param>
        private void ExecuteCommandLineProcess(string executableFile, string argumentList, string currentFolder, string archiveFolder, string triggerfilename)
        {
            //before starting process begin move all files to process folder
            
            // Use ProcessStartInfo class.
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = executableFile;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            startInfo.Arguments = argumentList;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using-statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();

                    // Register a log of the successful operation
                    CustomLogEvent(string.Format("Succesful operation --> Executable: {0} --> Arguments: {1}", executableFile, argumentList));

                    //upon completion move to archived folder.
                    FileManager fm = new FileManager();
                    foreach (String fi in Directory.EnumerateFiles(currentFolder))
                    {
                        FileInfo info = new FileInfo(fi);
                        fm.MoveFileforProcessing(info.Name, currentFolder, archiveFolder, Guid.Parse(argumentList.Split(' ')[1]));
                    }
                }
            }
            catch (Exception exc)
            {
                // Register a Log of the Exception
                CustomLogEvent(exc.Message);
            }
        }

        public static void CreateLogRecord(string message, string filename, Guid source_uid)
        {
            SOURCE_LOG sLog = new SOURCE_LOG();
            sLog.Batch_UID = Guid.NewGuid();
            sLog.Publisher = "Ingest.FileHandler";
            sLog.BusinessDate = GetStatementDateFromFilename(filename, source_uid);
            sLog.Category = "File.Watcher";
            sLog.Dataset = string.Empty;
            sLog.Elapsed = new TimeSpan(1);
            sLog.Type = "Info";
            sLog.Filename = filename.Substring(filename.LastIndexOf('\\') + 1).Split('.')[0];
            sLog.File_UID = Guid.Empty;
            sLog.Source_UID = source_uid;
            sLog.Log_UID = Guid.NewGuid();
            sLog.LogDate = DateTime.UtcNow;
            sLog.Message = message;
            Logging.Send(JsonConvert.SerializeObject(sLog));
        }
        public static DateTime? GetStatementDateFromFilename(string filename, Guid datasource_uid)
        {
            Regex reg = new Regex(@"\s\d{2}-?\d{2}-?\d{4}");


            if (datasource_uid == Guid.Parse("E4B30A37-D0BF-49D4-BEE9-A0CC19959E6D") && filename.Length > 0)
            {
                int month = int.Parse(filename.Substring(filename.LastIndexOf(" ") + 1, 2));
                int day = int.Parse(filename.Substring(filename.LastIndexOf(" ") + 3, 2));
                int year = int.Parse(filename.Substring(filename.LastIndexOf(" ") + 5, 4));

                DateTime result = new DateTime(year, month, day);
                return result;
            }
            else
            {
                foreach (Match m in reg.Matches(filename))
                {
                    DateTime result;
                    if (DateTime.TryParseExact(m.Value.Trim(), "mmmddyyyy", null, DateTimeStyles.None, out result))
                        return result;
                }
            }
            return null;
        }
        private bool CheckFile(string fileName, long firmid)
        {
            bool retValue = false;
            int linecount = File.ReadLines(fileName).Count();
            string lastLine = File.ReadLines(fileName).Last();
            CustomLogEvent($"File Name: {fileName}");
            CustomLogEvent($"line count: {linecount.ToString()}");
            CustomLogEvent($"last line value {lastLine}");
            if (lastLine.Contains((linecount - 3).ToString()))
            {
                retValue = true;
            }
            else
            {
                retValue = false;
                CreateWorkFlow(fileName,firmid);
            }

            return retValue;
        }
        #endregion

        private async void CreateWorkFlow(string filename, long FirmId)
        {
           WORK newWork = new WORK();
           newWork.Responsibility = "Internal.Ops";
           newWork.Procedure = "Feed.Receipt";
           newWork.Cause = "Data.Completeness";
           newWork.App = "Feed.Files";
           newWork.FirmId = FirmId;
           newWork.Description = $"Data is not complete in file: {filename} processing will not start.";
           newWork.Type = "Task";
           newWork.DueDate = DateTime.UtcNow;
           WORK_CONTEXT context = new WORK_CONTEXT();
           context.Message = $"Incomplete file - number of lines does not match the final count  - File Name: {filename}";
           context.Value = filename;
           newWork.Context = context;

           await Workflow.CreateWork($"[{JsonConvert.SerializeObject(newWork)}]", _log);
        }
    } 

    internal static class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<FolderWatcher>(service =>
                {
                    service.ConstructUsing(s => new FolderWatcher());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });
                //Setup Account that window service use to run.  
                configure.RunAsLocalSystem();
                configure.SetServiceName("NewportIngestionFileWatcher");
                configure.SetDisplayName("Newport Ingestion File Watcher");
                configure.SetDescription("Newport Ingestion File Watcher");
            });
        }
    }
}
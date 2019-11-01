using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Configuration;
using System.Xml.Serialization;

namespace FeedHandlerWatcher
{
    public partial class Service1 
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

        /// <summary>
        /// Name of the XML file where resides the specification of folders and extensions to be monitored
        /// </summary>
        private string fileNameXML;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Service1()
        {
            InitializeComponent();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Event automatically fired when the service is started by Windows
        /// </summary>
        /// <param name="args">array of arguments</param>
        public void Start()
        {
            // Initialize the list of filesystem Watchers based on the XML configuration file
            PopulateListFileSystemWatchers();

            // Start the file system watcher for each of the file specification and folders found on the List<>
            StartFileSystemWatcher();
        }

        /// <summary>
        /// Event automatically fired when the service is stopped by Windows
        /// </summary>
        public void Stop()
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

        #endregion

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
            foreach (CustomFolderSettings customFolder in listFolders)
            {
                DirectoryInfo dir = new DirectoryInfo(customFolder.FolderPath);

                // Checks whether the SystemWatcher is enabled and also the directory is a valid location
                if (customFolder.FolderEnabled && dir.Exists)
                {
                    // Creates a new instance of FileSystemWatcher
                    FileSystemWatcher fileSWatch = new FileSystemWatcher();

                    // Sets the filter
                    fileSWatch.Filter = customFolder.FolderFilter;

                    // Sets the folder location
                    fileSWatch.Path = customFolder.FolderPath;

                    // Sets the action to be executed
                    StringBuilder actionToExecute = new StringBuilder(customFolder.ExecutableFile);

                    // List of arguments
                    StringBuilder actionArguments = new StringBuilder(customFolder.ExecutableArguments);

                    // Subscribe to notify filters
                    fileSWatch.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

                    // Associate the event that will be triggered when a new file is added to the monitored folder, using a lambda Expression                    
                    fileSWatch.Created += (senderObj, fileSysArgs) => fileSWatch_Created(senderObj, fileSysArgs, actionToExecute.ToString(), actionArguments.ToString());

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
                    CustomLogEvent(string.Format("File system monitor cannot start because the folder ({0}) does not exist", customFolder.FolderPath));
                }
            }
        }

        /// <summary>
        /// This event is triggered when a file with the specified extension is created on the monitored folder
        /// </summary>
        /// <param name="sender">Object raising the event</param>
        /// <param name="e">List of arguments - FileSystemEventArgs</param>
        /// <param name="action_Exec">The action_ execute.</param>
        /// <param name="action_Args">The action_ arguments.</param>
        void fileSWatch_Created(object sender, FileSystemEventArgs e, string action_Exec, string action_Args)
        {
            // Gets the name of the file recently added
            string fileName = e.FullPath;

            // Adds the file name to the arguments.  The filename will be placed in lieu of {0}
            string newStr = string.Format(action_Args, fileName);

            // Executes the command from the DOS window
            ExecuteCommandLineProcess(action_Exec, newStr);
        }

        /// <summary>
        /// Record messages and logs into the Windows Event log
        /// </summary>
        /// <param name="message">Message to be recorded into the Windows Event log</param>
        private void CustomLogEvent(string message)
        {
            string eventSource = "testservoce";
            DateTime dt = new DateTime();
            dt = System.DateTime.UtcNow;
            message = dt.ToLocalTime() + ": " + message;

            EventLog.WriteEntry(eventSource, message);
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

            TextReader reader = new StreamReader(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + fileNameXML);
            object obj = deserializer.Deserialize(reader);

            // Close the TextReader object
            reader.Close();

            // Obtains a list of strongly typed CustomFolderSettings from XML Input data
            listFolders = obj as List<CustomFolderSettings>;
        }

        /// <summary>
        /// Executes a set of instructions through the command window
        /// </summary>
        /// <param name="executableFile">Name of the executable file or program</param>
        /// <param name="argumentList">List of arguments</param>
        private void ExecuteCommandLineProcess(string executableFile, string argumentList)
        {
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
                }
            }
            catch (Exception exc)
            {
                // Register a Log of the Exception
                CustomLogEvent(exc.Message);
            }
        }

        #endregion
    }
}

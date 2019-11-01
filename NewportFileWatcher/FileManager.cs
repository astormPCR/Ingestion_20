using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedHandlerWatcher
{
    public interface IFileManager
    {
        bool ArchiveFile(string fileName,string sourceFolder, string destinationFolder);
        bool UnzipFile(string fileName, string sourceFolder, string destinationFolder);
        bool MoveFileforProcessing(string fileName, string sourceFolder, string destinationFolder, Guid source_uid);
    }
    public class FileManager : IFileManager
    {
        /// <summary>
        /// not implemented for version 1.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="destinationFolder"></param>
        /// <returns></returns>
        public bool ArchiveFile(string fileName, string sourceFolder, string destinationFolder)
        {
            throw new NotImplementedException();
        }
        public bool MoveFileforProcessing(string fileName, string sourceFolder, string destinationFolder, Guid source_uid)
        {
            try
            {
                FolderWatcher.CustomLogEvent($"Beginning move of file: {fileName}");
                File.Move($"{sourceFolder}\\{fileName}", $"{destinationFolder}\\{fileName}");
                FolderWatcher.CustomLogEvent($"Move complete for file: {fileName}");
                return true;
            }
            catch (IOException iex)
            {
                FolderWatcher.CustomLogEvent($"Error occurred moving file {fileName} {Environment.NewLine} Error is:  {iex.Message} {Environment.NewLine} {iex.StackTrace}");
                FolderWatcher.CreateLogRecord($"Could not move file: {fileName} Error is:  {iex.Message} -- {iex.StackTrace}", fileName, source_uid);
                return false;
            }
            catch (Exception ex)
            {
                FolderWatcher.CustomLogEvent($"Error occurred moving file {fileName} {Environment.NewLine} Error is:  {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                FolderWatcher.CreateLogRecord($"Could not move file: {fileName} Error is:  {ex.Message} -- {ex.StackTrace}", fileName, source_uid);
                return false;
            }
        }
        
        /// <summary>
        /// not implmeented for version 1.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="destinationFolder"></param>
        /// <returns></returns>
        public bool UnzipFile(string fileName, string sourceFolder, string destinationFolder)
        {
            throw new NotImplementedException();
        }
    }
}

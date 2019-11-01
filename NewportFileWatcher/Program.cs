using System.ServiceProcess;
using System.Configuration;
using Topshelf;
using Topshelf.Configurators;

namespace FeedHandlerWatcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ConfigureService.Configure();
        }
    }
}

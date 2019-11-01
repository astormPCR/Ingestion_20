using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Xml;
using Newtonsoft.Json.Linq;
namespace FeedHandlerWatcher
{
    public class Logging
    {
        public static string environmentVariable = ConfigurationManager.AppSettings["environment"];
        public static string loggingURL = ConfigurationManager.AppSettings["LoggingURL"];
        public static HttpClient httpClient = new HttpClient(new HttpClientHandler() { UseCookies = false });
        public static async Task Send(string jsonSourceLog)
        {
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, loggingURL);
                request.Content = new StringContent($"{jsonSourceLog}", Encoding.UTF8, "application/json");
                var i = await httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   }
}
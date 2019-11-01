using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;


namespace Ingestion_20.API
{
    public static class Logger
    {
        private static string environmentVariable = ConfigurationManager.AppSettings["environment"];
        private static string loggingURL = ConfigurationManager.AppSettings["LoggingURL"];
        private static HttpClient httpClient = new HttpClient(new HttpClientHandler() { UseCookies = false });

        public static async Task<bool> Send(string jsonSourceLog)
        {
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
            bool returnvalue = false;
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, loggingURL);
                request.Content = new StringContent($"{jsonSourceLog}", Encoding.UTF8, "application/json");
                var i = await httpClient.SendAsync(request);
                returnvalue = true;
            }
            catch (Exception ex)
            {
                returnvalue = false;
            }

            return returnvalue;
        }
    }
}

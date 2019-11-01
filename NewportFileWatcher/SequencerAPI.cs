using System;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Serilog;


namespace FeedHandlerWatcher
{
    public class SequencerAPI
    {
        public static string getFilesURL = ConfigurationManager.AppSettings["getFilesURL"];
        public static HttpClient httpClient = new HttpClient(new HttpClientHandler() { UseCookies = false }) { Timeout = TimeSpan.FromMilliseconds(6000000) };

        public string GetFiles(Guid source_uid)
        {
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.DefaultConnectionLimit = 30;


            HttpResponseMessage response;
            string retValue = string.Empty;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, getFilesURL);
            request.Content = new StringContent(source_uid.ToString(), Encoding.UTF8, "application/json");
            try
            {
                response = httpClient.SendAsync(request).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    retValue = response.Content.ReadAsStringAsync().Result;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
               
                return "";
            }

            return $"{retValue}";
        }
    }
}

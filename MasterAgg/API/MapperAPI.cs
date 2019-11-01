using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MasterAgg.API
{
    public static class MapperAPI
    {
        private static HttpClient httpClient = new HttpClient(new HttpClientHandler() { UseCookies = false });

        //TO DO:  update these to use the application configuration file.
        private static string sequencerURL = "https://sequencerdev.azurewebsites.net/api/RuleSetSequencer?code=mZdFFB3AT/lFhX8ZN8UyxyKEDlpBnn6B6Ee35HL2OqSxGipKWcGFwg=="; // ConfigurationManager.AppSettings["sequencerURL"];
        private static string getRuleSetURL = "https://sequencerdev.azurewebsites.net/api/getRuleSet?code=zPEh1E58Barzu93/UdE0PNerw4qdbaqOodqVmJnPSDrqaIukJa1Zfw==";
        private static string getFilesURL = "https://sequencerdev.azurewebsites.net/api/getFiles?code=ZNb/E4MGCm9H5DAgkRTzXE/VCiqL9MFon7UJnqCEfjiFhWO3sNewQQ==";
        private static string getFileDefinitionURL = "https://sequencerdev.azurewebsites.net/api/getFileDefinition?code=D5GitsI2sQhw2bkPOiauDaUoV4GkaUEVcIWb0HveDeVgQB5DDCgO5Q==";
        public static async Task<string> SendToMapper(string inputJson)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{sequencerURL}");
                request.Content = new StringContent($"{inputJson}", Encoding.UTF8, "application/json");
                var i = httpClient.SendAsync(request).Result;
                if (i.StatusCode == HttpStatusCode.OK)
                {
                    string response = (await i.Content.ReadAsStringAsync());
                    return response;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                //Logger.Send(new MasterAgg.Models.SOURCE_LOG() {  })
                return null;
            }
        }

        /// <summary>
        /// return rules for given fileUid
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetMapperRules(string fileUid)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{getRuleSetURL}");
                request.Content = new StringContent($"{fileUid}", Encoding.UTF8, "application/json");
                var i = httpClient.SendAsync(request).Result;
                if (i.StatusCode == HttpStatusCode.OK)
                {
                    string response = (await i.Content.ReadAsStringAsync());
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                //Logger.Send(new MasterAgg.Models.SOURCE_LOG() {  })
                return null;
            }
        }
        /// <summary>
        /// currently returning "hard coded values" function app will be created to return lookups based on context.
        /// should be part of the mapping rules call
        /// </summary>
        /// <returns></returns>
        public static string GetLookups()
        {
            return @"{
                ""Buy|Equity"": 120
                            ,
                ""Sell|Equity"": 121
            }";
        }
    }
}

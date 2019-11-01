using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;


namespace MasterAgg.API
{
    public static class Work
    {
        private static string workflowURL = ConfigurationManager.AppSettings["WorkflowURL"];
        private static HttpClient httpClient = new HttpClient(new HttpClientHandler() { UseCookies = false });

        public static async Task CreateWork(string message)
        {
           
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.DefaultConnectionLimit = 30;
            HttpResponseMessage response;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, workflowURL);
            request.Content = new StringContent($"{message}", Encoding.UTF8, "application/json");
            try
            {
                response = await httpClient.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                   
                }
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        public static string BuildJson(string Value, bool leaveOpen = true)
        {
            JTokenWriter writer = new JTokenWriter();
            writer.WriteStartObject();
            writer.WritePropertyName("Workflows");
            writer.WriteStartArray();
            writer.WriteStartObject();
            writer.WritePropertyName("FirmID");
            writer.WriteValue(470);
            writer.WritePropertyName("Procedure");
            writer.WriteValue("Accounts.New");
            writer.WritePropertyName("Description");
            writer.WriteValue(Value);
            writer.WritePropertyName("DueDate");
            writer.WriteValue(DateTime.Now.ToUniversalTime());
            writer.WritePropertyName("App");
            writer.WriteValue("Ingestion");
            writer.WritePropertyName("Type");
            writer.WriteValue("Info");
            writer.WritePropertyName("Remediation");
            writer.WriteValue("Feed.Acc.Insert");
            writer.WritePropertyName("ClosedDate");
            if (leaveOpen)
                writer.WriteValue(DateTime.Now.ToUniversalTime());
            else
                writer.WriteValue(string.Empty);
            writer.WritePropertyName("CreateEmail");
            writer.WriteValue("devops@pcrinsights.com");
            writer.WritePropertyName("Responsibility");
            writer.WriteValue("Internal.Ops");
            writer.WritePropertyName("Cause");
            writer.WriteValue("Feed.Acc.Created");
            writer.WritePropertyName("Group_UID");
            writer.WriteValue("2CBEF1AC-82C0-4452-B119-A6A18F1BCC41");
            writer.WriteEndObject();
            writer.WriteEndArray();
            writer.WriteEndObject();

            return writer.Token.ToString();
        }
    }
}

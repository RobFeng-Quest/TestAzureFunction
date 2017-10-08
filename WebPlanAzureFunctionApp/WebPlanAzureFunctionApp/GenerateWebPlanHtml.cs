using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebPlanAzureFunctionApp.Models;

namespace WebPlanAzureFunctionApp
{
    public static class GenerateWebPlanHtml
    {
        [FunctionName("GenerateWebPlanHtml")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(WebHookType = "genericJson")]HttpRequestMessage req,
            [Queue("WebPlans")]IAsyncCollector<QueryPlan> outputPlan,
            TraceWriter log)
        {
            log.Info($"Plan Json Webhook was triggered by VS Code!");

            string jsonContent = await req.Content.ReadAsStringAsync();
            var queryPlan = JsonConvert.DeserializeObject<QueryPlan>(jsonContent);

            log.Info($"Plan {queryPlan.PlanId} received from {queryPlan.Email} for product {queryPlan.ProductId}");

            // Store plan into query for trigger other azure function
            await outputPlan.AddAsync(queryPlan);

            string html = $@"<html><head><title>Hello world!</title></head><body>" +
                $@"<h1>Hello World!</h1><p>Thank you for your order action! <strong>{queryPlan.Email}</strong></p></body></html>";

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(html);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}

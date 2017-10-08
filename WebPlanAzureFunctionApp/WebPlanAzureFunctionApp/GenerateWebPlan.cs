using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using WebPlanAzureFunctionApp.Models;

namespace WebPlanAzureFunctionApp
{

    public static class GenerateWebPlan
    {
        [FunctionName("GenerateWebPlan")]
        public static async Task<object> Run(
            [HttpTrigger(WebHookType = "genericJson")]HttpRequestMessage req,
            [Queue("WebPlans")]IAsyncCollector<QueryPlan> outputPlan,
            TraceWriter log)
        {
            log.Info($"Plan Webhook was triggered by VS Code!");

            string jsonContent = await req.Content.ReadAsStringAsync();
            var queryPlan = JsonConvert.DeserializeObject<QueryPlan>(jsonContent);

            log.Info($"Plan {queryPlan.PlanId} received from {queryPlan.Email} for product {queryPlan.ProductId}");

            await outputPlan.AddAsync(queryPlan);

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                message = $"Thank you for your action! {queryPlan.Email}"
            });
        }        
    }
}

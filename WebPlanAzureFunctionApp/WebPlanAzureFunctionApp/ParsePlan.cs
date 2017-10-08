using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using WebPlanAzureFunctionApp.Models;
using System.IO;

namespace WebPlanAzureFunctionApp
{
    public static class ParsePlan
    {
        [FunctionName("ParsePlan")]
        public static void Run(
            [QueueTrigger("WebPlans", Connection = "AzureWebJobsStorage")]QueryPlan queryPlan,
            TraceWriter log,
            IBinder binder)
        {
            log.Info($"Received an query plan: Plan {queryPlan.PlanId} from Product {queryPlan.ProductId}");

            var blob = new BlobAttribute($"parsedplans/{queryPlan.PlanId}.txt") { Connection = "AzureWebJobsStorage" };

            using (var outputBlob = binder.Bind<TextWriter>(blob))
            {
                outputBlob.WriteLine($"PlanId: {queryPlan.PlanId}");
                outputBlob.WriteLine($"ProductId: {queryPlan.ProductId}");
                outputBlob.WriteLine($"Email: {queryPlan.Email}");
                outputBlob.WriteLine($"Plan: {queryPlan.Plan}");
                outputBlob.WriteLine($"PurchaseDate: {System.DateTime.UtcNow}");

                var md5 = System.Security.Cryptography.MD5.Create();
                var hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(queryPlan.Email + "secret"));
                outputBlob.WriteLine($"SecretCode: {System.BitConverter.ToString(hash).Replace("-", "")}");
            }
        }
    }
}


using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System;

namespace HttpTriggerFunction
{
    public static class Function1
    {
        [FunctionName("AzureTriggerFunction")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }



        // Azure Timer Trigger
        //public static void Run([TimerTrigger("0/5 * * * * *")]TimerInfo myTimer, ILogger log)
        //{
        //    if (myTimer.IsPastDue)
        //    {
        //        log.LogInformation("Timer is running late!");
        //    }
        //    log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        //}

        //Queue Trigger

        //public static void Run(
        //[QueueTrigger("myqueue-items")] string myQueueItem,
        //ILogger log)
        //{
        //    log.LogInformation($"C# function processed: {myQueueItem}");
        //}

    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Company.Function
{
    public static class CosmosHttpTrigger
    {
        [FunctionName("CosmosHttpTrigger")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
            databaseName: "coreDB",
            collectionName: "MyItem",
            ConnectionStringSetting = "RESOURCECONNECTOR_TESTWEBAPPFUNCTIONSECRETCONNECTIONSUCCEEDED_CONNECTIONSTRING")]IEnumerable<MyItem> items,
            ILogger log)
        {
            IList<MyItem> list = new List<MyItem>();
            foreach (MyItem item in items)
            {
                list.Add(item);
            }
            return new OkObjectResult($"Get items: {JsonConvert.SerializeObject(list)}");
        }
    }

    public class MyItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

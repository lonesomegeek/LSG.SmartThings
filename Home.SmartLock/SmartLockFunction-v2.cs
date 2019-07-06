using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Home.SmartLock.Helpers;
using Home.SmartLock.Models;
using Home.SmartLock.Pages;
using Home.SmartLock.Services;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Home.SmartLock.Functions;

namespace Home.SmartLock
{
    public static class SmartLockFunction_v2
    {

        [FunctionName("SmartLockFunction-v2")]
        public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post", "patch", "put", "delete", Route = null)] HttpRequest request,
        ILogger logger)
        {
            var runner = new GenericSmartThingsFunction(
                new Functions.SmartLockFunction(logger),
                logger);
            return await runner.Run(request);
        }
    }
}

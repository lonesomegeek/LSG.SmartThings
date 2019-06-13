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

namespace Home.SmartLock
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", "patch", "put", "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            var requestHelper = new RequestHelper(req);
            log.LogInformation($"{requestHelper.Payload.lifecycle} lifecycle.");
            log.LogDebug($"Body: {requestHelper.Payload.body}");

            var evt = requestHelper.Payload;
            var lifecycle = evt.lifecycle;

            switch (lifecycle.ToString())
            {
                case "PING":
                    return new OkObjectResult(new { pingData = new { challenge = requestHelper.Payload.pingData.challenge } });
                case "CONFIGURATION":
                    return HandleConfigurationEvent(evt.configurationData);
                case "INSTALL":
                    HandleAction(evt.installData);
                    return new OkResult();
                case "UNINSTALL":
                    return new OkResult();
                case "UPDATE":
                    HandleAction(evt.updateData);
                    return new OkResult();
                case "EVENT":
                    HandleEventAction(evt.eventData);
                    return new OkResult();
                default:
                    return new BadRequestObjectResult($"Lifecycle {requestHelper.Payload.lifecycle} not supported");
            }
        }

        private static void HandleEventAction(dynamic data)
        {
            // if lock state is unlocked
            // start timer "*/X * * * * *"
            var eventType = data.events[0].eventType.ToString();

            if (eventType == "DEVICE_EVENT")
            {
                var deviceEventValue = data.events[0].deviceEvent.value.ToString();
                if (deviceEventValue == "unlocked")
                {
                    var client = new SmartThingsClient();
                    var installedAppId = data.installedApp.installedAppId.ToString();
                    var authToken = data.authToken.ToString();
                    var scheduleInterval = data.installedApp.config.scheduleInterval[0].stringConfig.value;

                    client.Schedule(
                        installedAppId,
                        authToken,
                        new
                        {
                            name = "SCHEDULE_DOOR_UNLOCKED",
                            cron = new
                            {
                                expression = $"*/{scheduleInterval} * * * ? *",
                                timezone = "GMT"
                            }
                        });
                }                
            } else if (eventType == "TIMER_EVENT")
            {
                var timerEventName = data.events[0].timerEvent.name;
                if (timerEventName == "SCHEDULE_DOOR_UNLOCKED")
                {
                    var client = new SmartThingsClient();
                    var deviceId = data.installedApp.config.doorLock[0].deviceConfig.deviceId.ToString();
                    var authToken = data.authToken.ToString();
                    var commands = new
                    {
                        commands = new List<dynamic>
                        {
                            new
                            {
                                component = "main",
                                capability = "lock",
                                command = "lock"
                            }                            
                        }
                    };
                    client.Command(deviceId, authToken, commands);
                    var installedAppId = data.installedApp.installedAppId.ToString();

                    client.Unschedule(installedAppId, authToken, "SCHEDULE_DOOR_UNLOCKED");
                    // TODO: delete schedule
                }
            }
        }

        private static void HandleAction(dynamic data)
        {
            var client = new SmartThingsClient();
            //var commands = new List<dynamic>
            //{
            //    new
            //    {
            //        command = "on",
            //        capability = "switch",
            //        component = "main"
            //    }
            //};
            //client.Actuate(                
            //    data.installedApp.config.colorLight[0].deviceConfig.deviceId.ToString(),
            //    data.authToken.ToString(),
            //    commands);

            client.Subscribe(
                data.installedApp.installedAppId.ToString(),
                data.authToken.ToString(),
                new
                {
                    sourceType = "DEVICE",
                    device = new
                    {
                        deviceId = "b24fb669-96ff-49b6-9e47-30616533bd6b",
                        componentId = "*",
                        capability = "*",
                        attribute = "*",
                        stateChangeOnly = true
                    }
                });

        }

        private static IActionResult HandleConfigurationEvent(dynamic configurationData)
        {
            var phase = configurationData.phase;
            var pageId = configurationData.pageId;
            var settings = configurationData.config;

            switch (phase.ToString())
            {
                case "INITIALIZE":
                    var initializer = new ConfigurationInitializeSetting
                    {
                        name = "Test name",
                        description = "Test description",
                        firstPageId = "1",
                        id = "app"
                    };
                    return new OkObjectResult(new { configurationData = new { initialize = initializer } });
                case "PAGE":
                    return new OkObjectResult(new { configurationData = new { page = new Page1() } });
                default:
                    return new BadRequestObjectResult($"Unsupported config phase: {phase}");
            }
            throw new NotImplementedException();
        }


        //        #r "Newtonsoft.Json"

        //using System.Net;
        //using Microsoft.AspNetCore.Mvc;
        //using Microsoft.Extensions.Primitives;
        //using Newtonsoft.Json;

        //public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
        //    {
        //        log.LogInformation("Running trigger");
        //        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //        log.LogInformation($"Body: {requestBody}");
        //        dynamic data = JsonConvert.DeserializeObject(requestBody);
        //        if (data.lifecycle == "PING")
        //        {
        //            var returnObject = new { pingData = new { challenge = data.pingData.challenge } };
        //            log.LogInformation($"PING: {returnObject}");

        //            return new OkObjectResult(returnObject);
        //        }
        //        else if (data.lifecycle == "CONFIGURATION")
        //        {
        //            log.LogInformation("In CONFIGURATION pipeline");
        //            log.LogInformation($"CONFIGURATION: {data.configurationData}");
        //            return new OkObjectResult(data.configurationData);
        //        }
        //        else if (data.lifecycle == "INITIALIZE")
        //        {
        //            log.LogInformation("In INITIALIZE pipeline");
        //            log.LogInformation($"INITIALIZE: {data.configurationData}");
        //            return new OkObjectResult(data.configurationData);

        //        }
        //        else log.LogInformation("Unable...");
        //        return null;

        //        // string name = req.Query["name"];

        //        // string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //        // dynamic data = JsonConvert.DeserializeObject(requestBody);
        //        // name = name ?? data?.name;

        //        // return name != null
        //        //     ? (ActionResult)new OkObjectResult($"Hello, {name}")
        //        //     : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        //    }

    }
}

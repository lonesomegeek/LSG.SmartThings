using Home.SmartLock.Helpers;
using Home.SmartLock.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Home.SmartLock.Functions
{
    public class GenericSmartThingsFunction
    {
        private readonly ISmartThingsWebhook _webhook;

        public GenericSmartThingsFunction(ISmartThingsWebhook webhook)
        {
            _webhook = webhook;
        }

        public async Task<IActionResult> Run(HttpRequest request, ILogger logger)
        {
            var requestHelper = new RequestHelper(request);
            var dataRaw = requestHelper.Payload;
            var data = requestHelper.GetPayloadObject<SmartThingsRequest>();

            logger.LogInformation($"{data.lifecycle} lifecycle.");
            logger.LogDebug($"Body: {requestHelper.Payload.body}");

            switch (data.lifecycle)
            {
                case "PING":
                    return new OkObjectResult(new { pingData = new { challenge = data.pingData.challenge } });
                case "CONFIGURATION":
                    return await HandleConfiguration(data.configurationData);
                case "INSTALL":
                    return await _webhook.Install(data.installData);
                case "UNINSTALL":
                    return await _webhook.Uninstall(data.uninstallData);
                case "UPDATE":
                    return await _webhook.Update(data.updateData);                    
                case "EVENT":
                    return await HandleEvent(data.eventData);
                default:
                    return new BadRequestObjectResult($"Lifecycle {data.lifecycle} not supported");
            }
        }

        private async Task<IActionResult> HandleConfiguration(Configurationdata configurationData)
        {

            var phase = configurationData.phase.ToString();
            var pageId = configurationData.pageId;
            var settings = configurationData.config;

            switch (phase)
            {
                case "INITIALIZE":
                    return await _webhook.ConfigurationInitialize(configurationData);
                case "PAGE":
                    return await _webhook.ConfigurationPage(configurationData);
                default:
                    return new BadRequestObjectResult($"Unsupported config phase: {phase}");
            }
            throw new NotImplementedException();
        }

        public async Task<IActionResult> HandleEvent(Eventdata data)
        {
            // TODO: Support iteration of events
            var eventData = data.events[0];
            var eventType = eventData.eventType.ToString();
            switch (eventType)
            {
                case "DEVICE_EVENT":
                    return await _webhook.EventDeviceEvent(data);
                case "TIMER_EVENT":
                    return await _webhook.EventTimerEvent(data);
                default:
                    return new BadRequestObjectResult($"Event type {eventType} not supported");
            }
        }
    }
}

using Home.SmartLock.Helpers;
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
            logger.LogInformation($"{requestHelper.Payload.lifecycle} lifecycle.");
            logger.LogDebug($"Body: {requestHelper.Payload.body}");

            var evt = requestHelper.Payload;
            var lifecycle = evt.lifecycle.ToString();

            switch (lifecycle)
            {
                case "PING":
                    return new OkObjectResult(new { pingData = new { challenge = requestHelper.Payload.pingData.challenge } });
                case "CONFIGURATION":
                    return await HandleConfiguration(evt.configurationData);
                case "INSTALL":
                    return await _webhook.Install(evt.installData);
                case "UNINSTALL":
                    return await _webhook.Uninstall(evt.uninstallData);
                case "UPDATE":
                    return await _webhook.Update(evt.updateData);                    
                case "EVENT":
                    return await HandleEvent(evt);
                default:
                    return new BadRequestObjectResult($"Lifecycle {requestHelper.Payload.lifecycle} not supported");
            }
        }

        private async Task<IActionResult> HandleConfiguration(dynamic configurationData)
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

        public async Task<IActionResult> HandleEvent(dynamic data)
        {
            // TODO: Support iteration of events
            var eventData = data.eventData.events[0];
            var eventType = eventData.eventType.ToString();
            switch (eventType)
            {
                case "DEVICE_EVENT":
                    return _webhook.EventDeviceEvent(data);
                case "TIMER_EVENT":
                    return _webhook.EventTimerEvent(data);
                default:
                    return new BadRequestObjectResult($"Event type {eventType} not supported");
            }
        }
    }
}

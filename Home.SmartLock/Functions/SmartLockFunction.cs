using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Home.SmartLock.Models;
using Home.SmartLock.Pages;
using Home.SmartLock.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Home.SmartLock.Functions
{
    public class SmartLockFunction : ISmartThingsWebhook
    {
        public async Task<IActionResult> ConfigurationInitialize(dynamic data)
        {
            var initializer = new ConfigurationInitializeSetting
            {
                name = "Test name",
                description = "Test description",
                firstPageId = "1",
                id = "app"
            };
            return new OkObjectResult(new { configurationData = new { initialize = initializer } });
        }

        public async Task<IActionResult> ConfigurationPage(dynamic data) => new OkObjectResult(new { configurationData = new { page = new Page1() } });
        
        public async Task<IActionResult> EventDeviceEvent(dynamic data)
        {
            var deviceEvent = data.eventData.events[0].deviceEvent;
            var deviceEventValue = deviceEvent.value.ToString();
            var client = new SmartThingsClient();
            var installedApp = data.eventData.installedApp;
            var installedAppId = installedApp.installedAppId.ToString();
            var authToken = data.eventData.authToken.ToString();

            if (deviceEventValue == "unlocked")
            {
                var scheduleInterval =
                    DateTime.Now.DayOfWeek >= DayOfWeek.Monday && DateTime.Now.DayOfWeek <= DayOfWeek.Friday ?
                        installedApp.config.scheduleIntervalWeekdays[0].stringConfig.value :
                        installedApp.config.scheduleIntervalWeekenddays[0].stringConfig.value;

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
            else if (deviceEventValue == "locked")
            {
                client.Unschedule(installedAppId, authToken, "SCHEDULE_DOOR_UNLOCKED");
            }
            return new OkResult();
        }

        public async Task<IActionResult> EventTimerEvent(dynamic data)
        {
            var eventData = data.eventData;
            var evt = eventData.events[0];
            var timerEventName = evt.timerEvent.name.ToString();
            if (timerEventName == "SCHEDULE_DOOR_UNLOCKED")
            {
                var client = new SmartThingsClient();
                var authToken = eventData.authToken.ToString();

                // check if door sensor is on/off
                var installedApp = eventData.installedApp;
                var installedAppId = installedApp.installedAppId.ToString();
                var installedAppConfig = installedApp.config;
                var doorSensor = installedAppConfig.doorSensor[0];
                var doorSensorId = doorSensor.deviceConfig.deviceId.ToString();
                var doorSensorStatus = JObject.Parse(client.Status(doorSensorId, authToken));
                if (doorSensorStatus.components.main.relaySwitch["switch"].value.ToString() == "on")
                {
                    var doorLock = installedAppConfig.doorLock[0];
                    var doorLockId = doorLock.deviceConfig.deviceId.ToString();
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
                    client.Command(doorLockId, authToken, commands);
                    client.Unschedule(installedAppId, authToken, "SCHEDULE_DOOR_UNLOCKED");
                    // TODO: delete schedule
                }
            }
            return new OkResult();
        }

        public async Task<IActionResult> Install(dynamic data) => HandleInstallAndUpdateEvent(data);
        

        private async Task<IActionResult> HandleInstallAndUpdateEvent(dynamic data)
        {
            var client = new SmartThingsClient();

            var eventData = data.eventData;
            var installedApp = data.installedApp;
            var installedAppId = installedApp.installedAppId.ToString();
            var installedAppConfig = installedApp.config;
            var doorLock = installedAppConfig.doorLock[0];
            var doorLockId = doorLock.deviceConfig.deviceId.ToString();

            client.Subscribe(
                data.installedApp.installedAppId.ToString(),
                data.authToken.ToString(),
                new
                {
                    sourceType = "DEVICE",
                    device = new
                    {
                        deviceId = doorLockId,
                        componentId = "*",
                        capability = "*",
                        attribute = "*",
                        stateChangeOnly = true
                    }
                });

            return new OkResult();
        }

        public async Task<IActionResult> Uninstall(dynamic data) => new OkResult();        

        public Task<IActionResult> Update(dynamic data) => HandleInstallAndUpdateEvent(data);
    }
}

using Home.SmartLock.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Home.SmartLock.Pages
{
    class Page1 : ConfigurationPage
    {
        public Page1()
        {
            this.pageId = "1";
            this.name = "Test name";
            this.complete = true;
            this.sections = new List<Section> {
                new Section
                {
                    name = "Lock automatically this door",
                    settings = new List<Setting>
                    {
                        new Setting
                        {
                            id = "doorLock",
                            name= "Which lock?",
                            description = "Tap to set",
                            type = "DEVICE",
                            required = true,
                            multiple = false,
                            capabilities = new List<string> { "lock" },
                            permissions = new List<string> { "r", "x" }
                        },
                        new Setting
                        {
                            id = "doorSensor",
                            name= "Which sensor?",
                            description = "Tap to set",
                            type = "DEVICE",
                            required = true,
                            multiple = false,
                            capabilities = new List<string> { "sensor", "contactSensor" },
                            permissions = new List<string> { "r", "x" }
                        },
                        new Setting
                        {
                            id = "scheduleInterval",
                            name = "After how long?",
                            description = "Type in time in minutes",
                            type = "NUMBER",
                            required = true
                        }
                    }
                }
            };
        }
    }
}

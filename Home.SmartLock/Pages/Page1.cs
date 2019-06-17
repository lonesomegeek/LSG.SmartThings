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
                            name= "Which switch?",
                            description = "Tap to set",
                            type = "DEVICE",
                            required = true,
                            multiple = false,
                            capabilities = new List<string> { "switch" },
                            permissions = new List<string> { "r", "x" }
                        },
                        new Setting
                        {
                            id = "scheduleIntervalWeekdays",
                            name = "After how long on weekdays?",
                            description = "Type in time in minutes",
                            type = "NUMBER",
                            required = true
                        },
                        new Setting
                        {
                            id = "scheduleIntervalWeekenddays",
                            name = "After how long on weekend days?",
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

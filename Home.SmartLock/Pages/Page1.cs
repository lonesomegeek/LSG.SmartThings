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
                new Section {
                    name = "for temp",
                    settings = new List<Setting>
                    {
                       new Setting
                       {
                           id ="zipCode",
                           name = "zipcode",
                           description = "enter zipcode",
                           type = "TEXT",
                           required = true
                       }
                    }
                },
                new Section
                {
                    name = "Set the color of this light",
                    settings = new List<Setting>
                    {
                        new Setting
                        {
                            id = "colorLight",
                            name= "Which color light?",
                            description = "Tap to set",
                            type = "DEVICE",
                            required = true,
                            multiple = false,
                            capabilities = new List<string> { "colorControl", "switch", "switchLevel" },
                            permissions = new List<string> { "r", "x" }
                        }
                    }
                }
            };
        }
    }
}

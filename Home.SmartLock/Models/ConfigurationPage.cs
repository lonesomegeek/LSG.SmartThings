using System;
using System.Collections.Generic;
using System.Text;

namespace Home.SmartLock.Models
{

    public class ConfigurationPage
    {
        public string pageId { get; set; }
        public string name { get; set; }
        public object nextPageId { get; set; }
        public object previousPageId { get; set; }
        public bool complete { get; set; }
        public List<Section> sections { get; set; }
    }

    public class Section
    {
        public string name { get; set; }
        public List<Setting> settings { get; set; }
    }

    public class Setting
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public bool required { get; set; }
        public bool multiple { get; set; }
        public List<Option> options { get; set; }
        public List<string> capabilities { get; set; }
        public List<string> permissions { get; set; }
    }

    public class Option
    {
        public string id { get; set; }
        public string name { get; set; }
    }

}

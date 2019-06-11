using System;
using System.Collections.Generic;
using System.Text;

namespace Home.SmartLock.Models
{

    public class SmartThingsResponse
    {
        public int statusCode { get; set; }
        public Eventdata eventData { get; set; }
        public Installdata installData { get; set; }
        public Updatedata updateData { get; set; }
        public Uninstalldata uninstallData { get; set; }
        public Configurationdata configurationData { get; set; }
        public Pingdata pingData { get; set; }
        public Oauthcallbackdata oauthCallbackData { get; set; }
    }

}

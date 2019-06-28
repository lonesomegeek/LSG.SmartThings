using System;
using System.Collections.Generic;
using System.Text;

namespace Home.SmartLock.Models
{
    public class SmartThingsRequest
    {
        public string lifecycle { get; set; }
        public string executionId { get; set; }
        public string locale { get; set; }
        public string version { get; set; }
        public Eventdata eventData { get; set; }
        public Installdata installData { get; set; }
        public Updatedata updateData { get; set; }
        public Uninstalldata uninstallData { get; set; }
        public Configurationdata configurationData { get; set; }
        public Pingdata pingData { get; set; }
        public Oauthcallbackdata oauthCallbackData { get; set; }
        public Settings settings { get; set; }
    }

    public class Eventdata
    {
        public string authToken { get; set; }
        public Installedapp installedApp { get; set; }
        public Event[] events { get; set; }
    }

    public class Installedapp
    {
        public string installedAppId { get; set; }
        public string locationId { get; set; }
        public dynamic config { get; set; }
        public string[] permissions { get; set; }
    }



    public class SmartLockFunctionConfig
    {
        public StringconfigParameter[] scheduleInterval { get; set; }
        public DeviceconfigParameter[] doorSensor { get; set; }
        public DeviceconfigParameter[] doorLock { get; set; }
        public StringconfigParameter[] scheduleIntervalWeekdays { get; set; }
        public StringconfigParameter[] scheduleIntervalWeekenddays { get; set; }

    }

    public class DeviceconfigParameter
    {
        public string valueType { get; set; }
        public Deviceconfig deviceConfig { get; set; }

    }

    public class StringconfigParameter
    {
        public string valueType { get; set; }
        public Stringconfig stringConfig { get; set; }
    }

    public class Stringconfig
    {
        public string value { get; set; }
    }



    public class Deviceconfig
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
    }

    public class Event
    {
        public string eventType { get; set; }
        public Deviceevent deviceEvent { get; set; }
        public Modeevent modeEvent { get; set; }
        public Timerevent timerEvent { get; set; }
        public Devicecommandsevent deviceCommandsEvent { get; set; }
    }

    public class Deviceevent
    {
        public string subscriptionName { get; set; }
        public string eventId { get; set; }
        public string locationId { get; set; }
        public string deviceId { get; set; }
        public string componentId { get; set; }
        public string capability { get; set; }
        public string attribute { get; set; }
        public string value { get; set; }
        public bool stateChange { get; set; }
    }

    public class Modeevent
    {
        public string modeId { get; set; }
    }

    public class Timerevent
    {
        public string eventId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public DateTime time { get; set; }
        public string expression { get; set; }
    }

    public class Devicecommandsevent
    {
        public string deviceId { get; set; }
        public string profileId { get; set; }
        public string externalId { get; set; }
        public Command[] commands { get; set; }
    }

    public class Command
    {
        public string componentId { get; set; }
        public string capability { get; set; }
        public string command { get; set; }
        public object[] arguments { get; set; }
    }

    public class Installdata
    {
        public string authToken { get; set; }
        public string refreshToken { get; set; }
        public Installedapp installedApp { get; set; }
    }


    public class Updatedata
    {
        public string authToken { get; set; }
        public string refreshToken { get; set; }
        public Installedapp installedApp { get; set; }
        public Previousconfig previousConfig { get; set; }
        public string[] previousPermissions { get; set; }
    }


    public class Previousconfig
    {
        public Property[] property1 { get; set; }
        public Property[] property2 { get; set; }
    }

    public class Property
    {
        public string valueType { get; set; }
        public Deviceconfig deviceConfig { get; set; }
    }

    public class Uninstalldata
    {
        public Installedapp installedApp { get; set; }
    }

    public class Configurationdata
    {
        public string installedAppId { get; set; }
        public string phase { get; set; }
        public string pageId { get; set; }
        public string previousPageId { get; set; }
        public Config config { get; set; }
    }

    public class Config
    {
        public Property[] property1 { get; set; }
        public Property[] property2 { get; set; }
    }

    public class Pingdata
    {
        public string challenge { get; set; }
    }

    public class Oauthcallbackdata
    {
        public string installedAppId { get; set; }
        public string urlPath { get; set; }
    }

    public class Settings
    {
        public string property1 { get; set; }
        public string property2 { get; set; }
    }

}

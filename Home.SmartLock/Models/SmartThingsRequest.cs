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
        public Config config { get; set; }
        public string[] permissions { get; set; }
    }

    public class Config
    {
        public Contactsensor[] contactSensor { get; set; }
        public Lightswitch[] lightSwitch { get; set; }
        public Minute[] minutes { get; set; }
    }

    public class Contactsensor
    {
        public string valueType { get; set; }
        public Deviceconfig deviceConfig { get; set; }
    }

    public class Deviceconfig
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
    }

    public class Lightswitch
    {
        public string valueType { get; set; }
        public Deviceconfig1 deviceConfig { get; set; }
    }

    public class Deviceconfig1
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
    }

    public class Minute
    {
        public string valueType { get; set; }
        public Stringconfig stringConfig { get; set; }
    }

    public class Stringconfig
    {
        public string value { get; set; }
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
        public Installedapp1 installedApp { get; set; }
    }

    public class Installedapp1
    {
        public string installedAppId { get; set; }
        public string locationId { get; set; }
        public Config1 config { get; set; }
        public string[] permissions { get; set; }
    }

    public class Config1
    {
        public Contactsensor1[] contactSensor { get; set; }
        public Lightswitch1[] lightSwitch { get; set; }
        public Minute1[] minutes { get; set; }
    }

    public class Contactsensor1
    {
        public string valueType { get; set; }
        public Deviceconfig2 deviceConfig { get; set; }
    }

    public class Deviceconfig2
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
    }

    public class Lightswitch1
    {
        public string valueType { get; set; }
        public Deviceconfig3 deviceConfig { get; set; }
    }

    public class Deviceconfig3
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
    }

    public class Minute1
    {
        public string valueType { get; set; }
        public Stringconfig1 stringConfig { get; set; }
    }

    public class Stringconfig1
    {
        public string value { get; set; }
    }

    public class Updatedata
    {
        public string authToken { get; set; }
        public string refreshToken { get; set; }
        public Installedapp2 installedApp { get; set; }
        public Previousconfig previousConfig { get; set; }
        public string[] previousPermissions { get; set; }
    }

    public class Installedapp2
    {
        public string installedAppId { get; set; }
        public string locationId { get; set; }
        public Config2 config { get; set; }
        public string[] permissions { get; set; }
    }

    public class Config2
    {
        public Contactsensor2[] contactSensor { get; set; }
        public Lightswitch2[] lightSwitch { get; set; }
        public Minute2[] minutes { get; set; }
    }

    public class Contactsensor2
    {
        public string valueType { get; set; }
        public Deviceconfig4 deviceConfig { get; set; }
    }

    public class Deviceconfig4
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
    }

    public class Lightswitch2
    {
        public string valueType { get; set; }
        public Deviceconfig5 deviceConfig { get; set; }
    }

    public class Deviceconfig5
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
    }

    public class Minute2
    {
        public string valueType { get; set; }
        public Stringconfig2 stringConfig { get; set; }
    }

    public class Stringconfig2
    {
        public string value { get; set; }
    }

    public class Previousconfig
    {
        public Property1[] property1 { get; set; }
        public Property2[] property2 { get; set; }
    }

    public class Property1
    {
        public string valueType { get; set; }
        public Deviceconfig6 deviceConfig { get; set; }
    }

    public class Deviceconfig6
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
    }

    public class Property2
    {
        public string valueType { get; set; }
        public Deviceconfig7 deviceConfig { get; set; }
    }

    public class Deviceconfig7
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
    }

    public class Uninstalldata
    {
        public Installedapp3 installedApp { get; set; }
    }

    public class Installedapp3
    {
        public string installedAppId { get; set; }
        public string locationId { get; set; }
        public Config3 config { get; set; }
        public string[] permissions { get; set; }
    }

    public class Config3
    {
        public Contactsensor3[] contactSensor { get; set; }
        public Lightswitch3[] lightSwitch { get; set; }
        public Minute3[] minutes { get; set; }
    }

    public class Contactsensor3
    {
        public string valueType { get; set; }
        public Deviceconfig8 deviceConfig { get; set; }
    }

    public class Deviceconfig8
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
    }

    public class Lightswitch3
    {
        public string valueType { get; set; }
        public Deviceconfig9 deviceConfig { get; set; }
    }

    public class Deviceconfig9
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
    }

    public class Minute3
    {
        public string valueType { get; set; }
        public Stringconfig3 stringConfig { get; set; }
    }

    public class Stringconfig3
    {
        public string value { get; set; }
    }

    public class Configurationdata
    {
        public string installedAppId { get; set; }
        public string phase { get; set; }
        public string pageId { get; set; }
        public string previousPageId { get; set; }
        public Config4 config { get; set; }
    }

    public class Config4
    {
        public Property11[] property1 { get; set; }
        public Property21[] property2 { get; set; }
    }

    public class Property11
    {
        public string valueType { get; set; }
        public Deviceconfig10 deviceConfig { get; set; }
    }

    public class Deviceconfig10
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
    }

    public class Property21
    {
        public string valueType { get; set; }
        public Deviceconfig11 deviceConfig { get; set; }
    }

    public class Deviceconfig11
    {
        public string deviceId { get; set; }
        public string componentId { get; set; }
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

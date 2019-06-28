using Home.SmartLock.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Home.SmartLock
{
    public interface ISmartThingsWebhook
    {
        Task<IActionResult> ConfigurationInitialize(Configurationdata data);
        Task<IActionResult> ConfigurationPage(Configurationdata data);
        Task<IActionResult> Install(dynamic data);
        Task<IActionResult> Uninstall(Uninstalldata data);
        Task<IActionResult> Update(dynamic data);
        Task<IActionResult> EventDeviceEvent(Eventdata data);
        Task<IActionResult> EventTimerEvent(Eventdata data);
    }
}

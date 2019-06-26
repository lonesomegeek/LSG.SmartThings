using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Home.SmartLock
{
    public interface ISmartThingsWebhook
    {
        Task<IActionResult> ConfigurationInitialize(dynamic data);
        Task<IActionResult> ConfigurationPage(dynamic data);
        Task<IActionResult> Install(dynamic data);
        Task<IActionResult> Uninstall(dynamic data);
        Task<IActionResult> Update(dynamic data);
        Task<IActionResult> EventDeviceEvent(dynamic data);
        Task<IActionResult> EventTimerEvent(dynamic data);
    }
}

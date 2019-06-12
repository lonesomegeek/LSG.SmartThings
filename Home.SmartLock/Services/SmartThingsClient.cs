using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Home.SmartLock.Services
{
    public class SmartThingsClient
    {
        public string Actuate(string deviceId, string token, dynamic commands)
        {
            var client = new RestClient("https://api.smartthings.com/v1");
            var request = new RestRequest($"/devices/{deviceId}/commands");
            request.AddJsonBody(commands);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = client.Execute(request, Method.POST);
            return response.Content;
        }

        public string Subscribe(string installedApp, string token, dynamic commands)
        {
            var client = new RestClient("https://api.smartthings.com/v1");
            var request = new RestRequest($"/installedapps/{installedApp}/subscriptions");
            request.AddJsonBody(commands);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = client.Execute(request, Method.POST);
            return response.Content;
        }
    }
}

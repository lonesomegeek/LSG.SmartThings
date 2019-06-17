using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Home.SmartLock.Helpers
{
    public class RequestHelper
    {
        public dynamic Payload { get; private set; }
        private HttpRequest _request;

        public RequestHelper(HttpRequest request)
        {
            _request = request;
            string requestBody = new StreamReader(_request.Body).ReadToEnd();
            string requestHeaderAuthorization = request.Headers["Authorization"];
            Payload = JsonConvert.DeserializeObject(requestBody);
        }
    }
}

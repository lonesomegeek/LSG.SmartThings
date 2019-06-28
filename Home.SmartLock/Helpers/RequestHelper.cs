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
        private readonly string _requestBody;
        private HttpRequest _request;

        public RequestHelper(HttpRequest request)
        {
            _request = request;
            _requestBody = new StreamReader(_request.Body).ReadToEnd();
            Payload = JsonConvert.DeserializeObject(_requestBody);
        }

        public T GetPayloadObject<T>() => (T)JsonConvert.DeserializeObject<T>(_requestBody);
    }
}

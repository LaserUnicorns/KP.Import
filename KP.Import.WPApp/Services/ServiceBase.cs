using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KP.Import.WPApp.Services
{
    public abstract class ServiceBase
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string Scheme { get; } = "http";
        private string Host { get; } = "kp-import.azurewebsites.net";
        private int Port { get; } = -1;
        //private string Host { get; } = "localhost";
        //private int Port { get; } = 19705;
        protected abstract string Controller { get; }

        private Uri GetUri(string methodName, Dictionary<string, object> @params)
        {
            if (@params == null) throw new ArgumentNullException(nameof(@params));

            return new UriBuilder(Scheme, Host, Port)
            {
                Path = $"api/{Controller}/{methodName}",
                Query = string.Join("&", @params.Select(x => $"{x.Key}={x.Value}"))
            }.Uri;
        }

        protected async Task<T> GetResponse<T>(string methodName, Dictionary<string, object> @params)
        {
            var uri = GetUri(methodName, @params);
            var response = await _httpClient.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<T>(response);
        }

        protected Task<HttpResponseMessage> Post(string methodName, object data)
        {
            var uri = GetUri(methodName, new Dictionary<string, object>());
            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return _httpClient.PostAsync(uri, content);
        }
    }
}
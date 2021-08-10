using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace QuartzService.HttpService
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient httpClient;
        private readonly IOptions<HttpServiceSettings> options;

        public HttpService(IOptions<HttpServiceSettings> options)
        {
            this.options = options;

            var handler = new HttpClientHandler()
            {
                UseDefaultCredentials = false,
                Credentials = System.Net.CredentialCache.DefaultCredentials,
                AllowAutoRedirect = true
            };
            httpClient = new HttpClient(handler);
            //{
            //    BaseAddress = new Uri(this.options.Value.BaseUri)
            //};
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public T GetRequestResult<T>(string urlController, string param)
        {
            try
            {
                var content = new StringContent(param, Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync(urlController, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    return result;
                }
                else
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopDienTu.Utilities.Constants;
using ShopDienTu.Utilities.Enum;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.ApiIntegration
{
    public class BaseApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        protected async Task<T> GetAsync<T>(string url)
        {
            return await this.Action<T>(url, TypeAction.GetAsync);
        }

        protected async Task<T> DeleteAsync<T>(string url)
        {
            return await this.Action<T>(url, TypeAction.DeleteAsync);
        }

        protected async Task<T> PostAsync<T>(string url, object obj)
        {
            return await this.Action<T>(url, TypeAction.PostAsync, obj);
        }

        protected async Task<T> PutAsync<T>(string url, object obj)
        {
            return await this.Action<T>(url, TypeAction.PutAsync, obj);
        }

        protected async Task<T> PathAsync<T>(string url, object obj)
        {
            return await this.Action<T>(url, TypeAction.PathAsync, obj);
        }

        private async Task<T> Action<T>(string url, TypeAction type, object obj = null)
        {
            var client = this.CreateClient();
            var response = new HttpResponseMessage();

            if (type == TypeAction.GetAsync || type == TypeAction.DeleteAsync)
            {
                if (type == TypeAction.GetAsync)
                    response = await client.GetAsync(url);
                else
                    response = await client.DeleteAsync(url);
            }

            if (type == TypeAction.PostAsync || type == TypeAction.PutAsync || type == TypeAction.PathAsync)
            {
                var json = JsonConvert.SerializeObject(obj);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                if (type == TypeAction.PostAsync)
                    response = await client.PostAsync(url, httpContent);
                else if (type == TypeAction.PutAsync)
                    response = await client.PutAsync(url, httpContent);
                else
                    response = await client.PatchAsync(url, httpContent);
            }

            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                T myDeserializedObjList = (T)JsonConvert.DeserializeObject(body,
                    typeof(T));

                return myDeserializedObjList;
            }
            return JsonConvert.DeserializeObject<T>(body);
        }

        public HttpClient CreateClient()
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            return client;
        }
    }
}

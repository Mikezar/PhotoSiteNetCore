using PhotoSite.ManagementBoard.Models;
using PhotoSite.ManagementBoard.Services.Abstract;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Implementation
{
    internal sealed class HttpHandler : IHttpHandler
    {
        private readonly HttpClient _httpClient;
        private readonly SessionStorage _storage;

        public HttpHandler(SessionStorage storage, HttpClient httpClient)
        {
            _storage = storage;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-CUSTOM-TOKEN", _storage.Token);
        }

        public async Task<NoResultWrapper> PostAsync(string method)
            => await PostAsync(method, null);

        public async Task<NoResultWrapper> PostAsync(string method, object model)
        {
            var response = await _httpClient.PostAsync(method, GetContent(model));
            return HandleResponse(response);
        }

        public async Task<ResultWrapper<TResult>> PostAsync<TResult>(string method, object model)
        {
            var response = await _httpClient.PostAsync(method, GetContent(model));
            return await HandleResponse<TResult>(response);
        }

        private async Task<ResultWrapper<TResult>> HandleResponse<TResult>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return ResultWrapper<TResult>.CreateFailed();
            }

            var json = await response.Content.ReadAsStringAsync();

            return new ResultWrapper<TResult>()
            {
                IsSuccess = true,
                Result = JsonSerializer.Deserialize<TResult>(json)
            };
        }

        private NoResultWrapper HandleResponse(HttpResponseMessage response)
        {
            return new NoResultWrapper
            {
                IsSuccess = response.IsSuccessStatusCode
            };
        }

        private static HttpContent GetContent(object model)
            => new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
    }
}

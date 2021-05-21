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

        public async Task<ResultWrapper<TResult>> GetAsync<TResult>(string method)
        {
            var response = await _httpClient.GetAsync(method);
            return await HandleResponse<TResult>(response);
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

        public async Task<NoResultWrapper> PutAsync(string method, object model)
        {
            var response = await _httpClient.PutAsync(method, GetContent(model));
            return HandleResponse(response);
        }

        public async Task<NoResultWrapper> DeleteAsync(string method)
        {
            var response = await _httpClient.DeleteAsync(method);
            return HandleResponse(response);
        }

        private async Task<ResultWrapper<TResult>> HandleResponse<TResult>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return ResultWrapper<TResult>.CreateFailed(response.StatusCode);
            }

            var json = await response.Content.ReadAsStringAsync();
            var payload = JsonSerializer.Deserialize<TResult>(json);

            return ResultWrapper<TResult>.CreateSuccess(payload);
        }

        private NoResultWrapper HandleResponse(HttpResponseMessage response)
        {
            return new NoResultWrapper
            {
                IsSuccess = response.IsSuccessStatusCode,
                Code = response.StatusCode
            };
        }

        private static HttpContent GetContent(object model)
            => new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
    }
}

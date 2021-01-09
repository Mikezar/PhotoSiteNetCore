using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using PhotoSite.Data.Base;
using PhotoSite.WebApi.Admin.Authorize;
using PhotoSite.WebApi.Configuration;
using PhotoSite.WebApi.Options;
using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests.Base
{
    public class BaseTestServerFixture : IDisposable
    {
        public TestServer TestServer { get; }
        public MainDbContext DbContext { get; }
        public CustomTokenAuthOptions TokenAuthOptions { get; } = new CustomTokenAuthOptions();
        protected internal HttpClient AdminClient { get; }

        public BaseTestServerFixture()
        {
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;
            var contentRoot = GetProjectPath(startupAssembly);

            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .CustomConfigureAppConfiguration(contentRoot)
                .ConfigureServices(
                    services => services.AddSingleton<IStartupFilter, FakeRemoteIpAddressStartupFilter>())
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            DbContext = TestServer.Host.Services.GetService<MainDbContext>();

            // Because can one Admin connection
            AdminClient = Task.Run(async() => await GetAdminClient()).Result;
        }

        public void Dispose()
        {
            //Task.Run(async () => await LogoutAdmin()).Wait();
            AdminClient.Dispose();
            TestServer.Dispose();
        }

        public async Task LogoutAdmin()
        {
            var response = await AdminClient.PostAsync("/api/admin/logout", new StringContent(string.Empty));
            Assert.True(response.IsSuccessStatusCode);
            response = await AdminClient.GetAsync("/api/admin/logout");
            Assert.False(response.IsSuccessStatusCode);
        }

        private static string GetProjectPath(Assembly startupAssembly)
        {
            var projectName = startupAssembly.GetName().Name!;
            var applicationBasePath = AppContext.BaseDirectory;
            var directoryInfo = new DirectoryInfo(applicationBasePath);

            while (directoryInfo.Parent != null)
            {
                directoryInfo = directoryInfo.Parent;
                var projectDirectoryInfo = new DirectoryInfo(directoryInfo.FullName);
                if (projectDirectoryInfo.Exists &&
                    new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj"))
                        .Exists)
                    return Path.Combine(projectDirectoryInfo.FullName, projectName);
            }

            throw new Exception($"Can't determine project root directory inside {applicationBasePath}");
        }

        private async Task<HttpClient> GetAdminClient()
        {
            var client = TestServer.CreateClient();
            var token = await Login(client);
            client.DefaultRequestHeaders.Add(TokenAuthOptions.TokenHeaderName, token);
            return client;
        }

        public HttpClient GetUserClient()
        {
            return TestServer.CreateClient();
        }

        private async Task<string> Login(HttpClient client)
        {
            var data = new LoginDto
            {
                Login = "Test",
                Password = "123456"
            };
            var json = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/admin/login", stringContent);
            response.EnsureSuccessStatusCode();
            var models = JsonSerializer.Deserialize<LoginStateDto>(await response.Content.ReadAsStringAsync());
            if (models?.Status != LoginStatusDto.Success)
                throw new Exception("Login failed");
            return models.Token!;
        }

        internal async Task UserUnauthorizedPostTest(string requestUri)
        {
            using var client = GetUserClient();
            var stringContent = new StringContent("{}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, stringContent);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        internal async Task UserUnauthorizedGetTest(string requestUri)
        {
            using var client = GetUserClient();
            var response = await client.GetAsync(requestUri);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        internal StringContent GetStringContent<T>(T value)
        {
            var json = JsonSerializer.Serialize(value);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        internal async Task<TResult?> GetAsync<TResult>(HttpClient client, string uri) where TResult : class
        {
            var response = await client.GetAsync(uri);
            Assert.True(response.IsSuccessStatusCode);
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(json))
                return null;
            return JsonSerializer.Deserialize<TResult>(json);
        }

        internal async Task<TResult?> PostAsync<TModel, TResult>(HttpClient client, string uri, TModel value) where TResult : class
        {
            var response = await client.PostAsync(uri, GetStringContent(value));
            Assert.True(response.IsSuccessStatusCode);
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(json))
                return null;
            return JsonSerializer.Deserialize<TResult>(json);
        }
    }
}
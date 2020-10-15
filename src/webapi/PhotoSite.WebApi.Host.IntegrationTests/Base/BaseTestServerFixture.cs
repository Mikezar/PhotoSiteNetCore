using System;
using System.IO;
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

namespace PhotoSite.WebApi.Host.IntegrationTests.Base
{
    public class BaseTestServerFixture
    {
        public TestServer TestServer { get; }
        public MainDbContext DbContext { get; }
        public CustomTokenAuthOptions TokenAuthOptions { get; } = new CustomTokenAuthOptions();

        public BaseTestServerFixture()
        {
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;
            var contentRoot = GetProjectPath(startupAssembly);

            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .CustomConfigureAppConfiguration(contentRoot)
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            DbContext = TestServer.Host.Services.GetService<MainDbContext>();
        }

        public void Dispose()
        {
            TestServer.Dispose();
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

        public async Task<HttpClient> GetAdminClient()
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
            var response = await client.PostAsync("/api/ad/login", stringContent);
            response.EnsureSuccessStatusCode();
            var models = JsonSerializer.Deserialize<LoginStateDto>(await response.Content.ReadAsStringAsync());
            if (models.Status != LoginStatusDto.Success)
                throw new Exception("Login failed");
            return models.Token!;
        }
    }
}
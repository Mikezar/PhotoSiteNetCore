using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PhotoSite.WebApi.Admin;
using PhotoSite.WebApi.Host.IntegrationTests.Base;
using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests
{
    public class SettingControllerTest : IClassFixture<BaseTestServerFixture>
    {
        private readonly BaseTestServerFixture _fixture;

        public SettingControllerTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetDefaultSettings()
        {
            using var client = await _fixture.GetAdminClient();
            var response = await client.GetAsync("/api/adj/default");
            response.EnsureSuccessStatusCode();
            var models = JsonSerializer.Deserialize<SettingsDto>(await response.Content.ReadAsStringAsync());
            Assert.Equal(80, models.Alpha);
        }

        [Fact]
        public async Task GetSettings()
        {
            using var client = await _fixture.GetAdminClient();

            var model = await GetSettingDto(client);
            Assert.Equal(80, model.Alpha);

            model.Alpha = 88;
            var json = JsonSerializer.Serialize(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/adj/settings", stringContent);
            response.EnsureSuccessStatusCode();

            model = await GetSettingDto(client);
            Assert.Equal(88, model.Alpha);
        }

        private async Task<SettingsDto> GetSettingDto(HttpClient client)
        {
            var response = await client.GetAsync("/api/adj/settings");
            response.EnsureSuccessStatusCode();
            var models = JsonSerializer.Deserialize<SettingsDto>(await response.Content.ReadAsStringAsync());
            return models;
        }

    }
}
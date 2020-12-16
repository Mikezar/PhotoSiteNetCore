using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PhotoSite.WebApi.Admin;
using PhotoSite.WebApi.Host.IntegrationTests.Base;
using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests
{
    [Collection("Test collection")]
    public class SettingControllerTest
    {
        private readonly BaseTestServerFixture _fixture;

        public SettingControllerTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetDefaultSettings()
        {
            var client = _fixture.AdminClient;
            var model = await _fixture.GetAsync<SettingsDto>(client, "/api/adj/default");
            Assert.Equal(60, model.WatermarkFontSize);
        }

        [Fact]
        public async Task Settings()
        { 
            var client = _fixture.AdminClient;

            var model = await GetSettingDto(client);
            Assert.Equal(80, model.Alpha);

            model.Alpha = 88;
            var response = await client.PostAsync("/api/adj/settings", _fixture.GetStringContent(model));
            Assert.True(response.IsSuccessStatusCode);

            model = await GetSettingDto(client);
            Assert.Equal(88, model.Alpha);
        }

        private async Task<SettingsDto> GetSettingDto(HttpClient client)
        {
            var response = await client.GetAsync("/api/adj/settings");
            Assert.True(response.IsSuccessStatusCode);
            var models = JsonSerializer.Deserialize<SettingsDto>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(models);
            return models!;
        }

        [Fact]
        public async Task UserUnauthorizedDefaultTest()
        {
            await _fixture.UserUnauthorizedGetTest("/api/adj/default");
        }

        [Fact]
        public async Task UserUnauthorizedPostTest()
        {
            await _fixture.UserUnauthorizedPostTest("/api/adj/settings");
        }

        [Fact]
        public async Task UserUnauthorizedGetTest()
        {
            await _fixture.UserUnauthorizedGetTest("/api/adj/settings");
        }

    }
}
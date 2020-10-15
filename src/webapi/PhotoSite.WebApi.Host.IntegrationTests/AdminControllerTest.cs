using PhotoSite.WebApi.Host.IntegrationTests.Base;
using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests
{
    public class AdminControllerTest : IClassFixture<BaseTestServerFixture>
    {

        private readonly BaseTestServerFixture _fixture;

        public AdminControllerTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        //[Fact]
        //public async Task LoginTest()
        //{
        //    var data = new LoginDto
        //    {
        //        Login = "Test",
        //        Password = "123456"
        //    };
        //    var json = JsonSerializer.Serialize(data);
        //    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await _fixture.Client.PostAsync("/api/ad/login", stringContent);
        //    response.EnsureSuccessStatusCode();
        //    var models = JsonSerializer.Deserialize<LoginStateDto>(await response.Content.ReadAsStringAsync());
        //    Assert.Equal(LoginStatusDto.Success, models.Status);

        //}

    }
}

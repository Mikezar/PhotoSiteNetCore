using System.Threading.Tasks;
using PhotoSite.WebApi.Host.IntegrationTests.Base;
using PhotoSite.WebApi.Photo;
using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests
{
    [Collection("Test collection")]
    public class WatermarkControllerTest
    {
        private const string ApiName = "api/watermark";

        private readonly BaseTestServerFixture _fixture;

        private const int TestPhotoId = 1;

        public WatermarkControllerTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ComplexTest()
        {
            var client = _fixture.AdminClient;
            var model = new WatermarkDto { PhotoId = TestPhotoId, IsRightSide = true};
            await _fixture.PostAsync(client, $"{ApiName}", model);

            model = await _fixture.GetAsync<WatermarkDto>(client, $"{ApiName}/{TestPhotoId}");
            Assert.NotNull(model);
            Assert.True(model!.IsRightSide);

            model = new WatermarkDto { PhotoId = TestPhotoId, IsRightSide = false };
            await _fixture.PutAsync(client, $"{ApiName}", model);

            model = await _fixture.GetAsync<WatermarkDto>(client, $"{ApiName}/{TestPhotoId}");
            Assert.NotNull(model);
            Assert.False(model!.IsRightSide);
        }

        [Fact]
        public async Task UserUnauthorizedUpdateTest()
        {
            await _fixture.UserUnauthorizedPostTest($"{ApiName}");
        }

        [Fact]
        public async Task UserUnauthorizedCreateTest()
        {
            await _fixture.UserUnauthorizedPostTest($"{ApiName}");
        }

        [Fact]
        public async Task UserUnauthorizedByPhotoIdTest()
        {
            await _fixture.UserUnauthorizedGetTest($"{ApiName}/0");
        }
    }
}
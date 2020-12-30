using System.Threading.Tasks;
using PhotoSite.WebApi.Common;
using PhotoSite.WebApi.Host.IntegrationTests.Base;
using PhotoSite.WebApi.Photo;
using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests
{
    [Collection("Test collection")]
    public class WatermarkControllerTest
    {
        private const string ApiName = "api/wm/";

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
            await _fixture.PostAsync<WatermarkDto, ResultDto>(client, $"{ApiName}create", model);

            model = await _fixture.GetAsync<WatermarkDto>(client, $"{ApiName}byphoto?photoId={TestPhotoId}");
            Assert.NotNull(model);
            Assert.True(model!.IsRightSide);

            model = new WatermarkDto { PhotoId = TestPhotoId, IsRightSide = false };
            await _fixture.PostAsync<WatermarkDto, ResultDto>(client, $"{ApiName}update", model);

            model = await _fixture.GetAsync<WatermarkDto>(client, $"{ApiName}byphoto?photoId={TestPhotoId}");
            Assert.NotNull(model);
            Assert.False(model!.IsRightSide);
        }

        [Fact]
        public async Task UserUnauthorizedUpdateTest()
        {
            await _fixture.UserUnauthorizedPostTest($"{ApiName}update");
        }

        [Fact]
        public async Task UserUnauthorizedCreateTest()
        {
            await _fixture.UserUnauthorizedPostTest($"{ApiName}create");
        }

        [Fact]
        public async Task UserUnauthorizedByPhotoIdTest()
        {
            await _fixture.UserUnauthorizedGetTest($"{ApiName}byphoto?photoId=0");
        }
    }
}
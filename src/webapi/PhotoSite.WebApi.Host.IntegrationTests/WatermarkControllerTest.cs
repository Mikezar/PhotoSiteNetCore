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
            await _fixture.PostAsync<WatermarkDto, IdResultDto>(client, "api/wm/create", model);

            model = await _fixture.GetAsync<WatermarkDto>(client, $"/api/wm/byphoto?photoId={TestPhotoId}");
            Assert.NotNull(model);
            Assert.True(model.IsRightSide);

            model = new WatermarkDto { PhotoId = TestPhotoId, IsRightSide = false };
            await _fixture.PostAsync<WatermarkDto, IdResultDto>(client, "api/wm/update", model);

            model = await _fixture.GetAsync<WatermarkDto>(client, $"/api/wm/byphoto?photoId={TestPhotoId}");
            Assert.NotNull(model);
            Assert.False(model.IsRightSide);
        }
    }
}
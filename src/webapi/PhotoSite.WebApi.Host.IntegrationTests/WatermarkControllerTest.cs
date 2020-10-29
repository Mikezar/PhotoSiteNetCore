using System.Threading.Tasks;
using PhotoSite.WebApi.Common;
using PhotoSite.WebApi.Host.IntegrationTests.Base;
using PhotoSite.WebApi.Photo;
using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests
{
    public class WatermarkControllerTest : IClassFixture<BaseTestServerFixture>
    {
        private readonly BaseTestServerFixture _fixture;

        private const int TestPhotoId1 = 1;
        private const int TestPhotoId2 = 2;

        public WatermarkControllerTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ComplexTest()
        {
            using var client = await _fixture.GetAdminClient();
            var model = new WatermarkDto { PhotoId = TestPhotoId1 };
            await _fixture.PostAsync<WatermarkDto, IdResultDto>(client, "api/wm/create", model);

            model = await _fixture.GetAsync<WatermarkDto>(client, $"/api/wm/byphoto?photoId={TestPhotoId1}");
            Assert.NotNull(model);
            Assert.Equal(TestPhotoId1, model.PhotoId);

            model = new WatermarkDto { Id = model.Id, PhotoId = TestPhotoId1 };
            await _fixture.PostAsync<WatermarkDto, IdResultDto>(client, "api/wm/update", model);

            model = await _fixture.GetAsync<WatermarkDto>(client, $"/api/wm/byphoto?photoId={TestPhotoId1}");
            Assert.NotNull(model);
            Assert.Equal(TestPhotoId2, model.PhotoId);
        }
    }
}
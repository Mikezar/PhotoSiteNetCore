using System.Threading.Tasks;
using PhotoSite.WebApi.Host.IntegrationTests.Base;
using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests
{
    [Collection("Test collection")]
    public class PhotoToTagControllerTest
    {
        private const string ApiName = "api/photototag/";

        private readonly BaseTestServerFixture _fixture;

        public PhotoToTagControllerTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ComplexTest() // TODO: Do
        {
            var client = _fixture.AdminClient;
            //var model = new WatermarkDto { PhotoId = TestPhotoId, IsRightSide = true };
            //await _fixture.PostAsync<WatermarkDto, IdResultDto>(client, "api/wm/create", model);

            //model = await _fixture.GetAsync<WatermarkDto>(client, $"/api/wm/byphoto?photoId={TestPhotoId}");
            //Assert.NotNull(model);
            //Assert.True(model!.IsRightSide);

            //model = new WatermarkDto { PhotoId = TestPhotoId, IsRightSide = false };
            //await _fixture.PostAsync<WatermarkDto, IdResultDto>(client, "api/wm/update", model);

            //model = await _fixture.GetAsync<WatermarkDto>(client, $"/api/wm/byphoto?photoId={TestPhotoId}");
            //Assert.NotNull(model);
            //Assert.False(model!.IsRightSide);
        }

        [Fact]
        public async Task UserUnauthorizedGetByPhotoIdTest()
        {
            await _fixture.UserUnauthorizedGetTest($"{ApiName}byphoto/0");
        }

        [Fact]
        public async Task UserUnauthorizedGetNotExistsInPhotoTest()
        {
            await _fixture.UserUnauthorizedGetTest($"{ApiName}notbyphoto/0");
        }

        [Fact]
        public async Task UserUnauthorizedBindTagsToPhotoTest()
        {
            await _fixture.UserUnauthorizedPostTest($"{ApiName}bindtagphoto");
        }
    }
}
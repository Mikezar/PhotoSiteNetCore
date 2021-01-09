using System.Threading.Tasks;
using PhotoSite.WebApi.Album;
using PhotoSite.WebApi.Common;
using PhotoSite.WebApi.Host.IntegrationTests.Base;
using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests
{
    [Collection("Test collection")]
    public class AlbumControllerTest
    {
        internal const string ApiName = "api/album/";

        private readonly BaseTestServerFixture _fixture;

        public AlbumControllerTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task ComplexTest()
        {
            const string testTitle = "TestAlbum";
            const string updateTestTitle = "UpdateTestAlbum";

            var client = _fixture.AdminClient;
            var model = new AlbumDto { Title = testTitle };
            await _fixture.PostAsync<AlbumDto, IdResultDto>(client, $"{ApiName}create", model);

            var models = await _fixture.GetAsync<AlbumDto[]>(client, $"{ApiName}getchildren");
            Assert.NotNull(models);
            Assert.Single(models!);
            Assert.Equal(testTitle, models![0].Title);

            model = new AlbumDto { Id = models[0].Id, Title = updateTestTitle };
            await _fixture.PostAsync<AlbumDto, ResultDto>(client, $"{ApiName}update", model);

            models = await _fixture.GetAsync<AlbumDto[]>(client, $"{ApiName}getchildren");
            Assert.NotNull(models);
            Assert.Single(models!);
            Assert.Equal(updateTestTitle, models![0].Title);

            await _fixture.PostAsync<AlbumDto, ResultDto>(client, $"{ApiName}delete", model);
            models = await _fixture.GetAsync<AlbumDto[]>(client, $"{ApiName}getchildren");
            Assert.Null(models);
        }

        [Fact]
        public async Task UserUnauthorizedDeleteTest()
        {
            await _fixture.UserUnauthorizedPostTest($"{ApiName}delete");
        }

        [Fact]
        public async Task UserUnauthorizedCreateTest()
        {
            await _fixture.UserUnauthorizedPostTest($"{ApiName}create");
        }

        [Fact]
        public async Task UserUnauthorizedUpdateTest()
        {
            await _fixture.UserUnauthorizedPostTest($"{ApiName}update");
        }
    }
}
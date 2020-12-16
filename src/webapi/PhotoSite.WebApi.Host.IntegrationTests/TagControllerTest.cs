using System.Threading.Tasks;
using PhotoSite.WebApi.Common;
using PhotoSite.WebApi.Host.IntegrationTests.Base;
using PhotoSite.WebApi.Photo;
using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests
{
    [Collection("Test collection")]
    public class TagControllerTest
    {
        private readonly BaseTestServerFixture _fixture;

        private const string TestTitle = "TestTag";
        private const string UpdateTestTitle = "UpdateTestTag";

        public TagControllerTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ComplexTest()
        {
            var client = _fixture.AdminClient;
            var model = new TagDto {Title = TestTitle };
            await _fixture.PostAsync<TagDto, IdResultDto>(client,"/api/tag/create", model);

            var models = await _fixture.GetAsync<TagDto[]>(client, "/api/tag/getall");
            Assert.NotNull(models);
            Assert.Single(models!);
            Assert.Equal(TestTitle, models![0].Title);

            model = new TagDto { Id = models[0].Id, Title = UpdateTestTitle };
            await _fixture.PostAsync<TagDto, ResultDto>(client, "/api/tag/update", model);

            models = await _fixture.GetAsync<TagDto[]>(client, "/api/tag/getall");
            Assert.NotNull(models);
            Assert.Single(models!);
            Assert.Equal(UpdateTestTitle, models![0].Title);
        }

        [Fact]
        public async Task UserUnauthorizedCreateTest()
        {
            await _fixture.UserUnauthorizedPostTest("/api/tag/create");
        }

        [Fact]
        public async Task UserUnauthorizedUpdateTest()
        {
            await _fixture.UserUnauthorizedPostTest("/api/tag/update");
        }

    }
}
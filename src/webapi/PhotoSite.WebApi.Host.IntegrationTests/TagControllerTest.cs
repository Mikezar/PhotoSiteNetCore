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
        private const string ApiName = "api/tag/";

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
            await _fixture.PostAsync<TagDto, IdResultDto>(client, $"{ApiName}create", model);

            var models = await _fixture.GetAsync<TagDto[]>(client, $"{ApiName}getall");
            Assert.NotNull(models);
            Assert.Single(models!);
            Assert.Equal(TestTitle, models![0].Title);

            model = new TagDto { Id = models[0].Id, Title = UpdateTestTitle };
            await _fixture.PostAsync<TagDto, ResultDto>(client, $"{ApiName}update", model);

            models = await _fixture.GetAsync<TagDto[]>(client, $"{ApiName}getall");
            Assert.NotNull(models);
            Assert.Single(models!);
            Assert.Equal(UpdateTestTitle, models![0].Title);
        }

        [Fact]
        public async Task UserUnauthorizedGetAllTest()
        {
            await _fixture.UserUnauthorizedGetTest($"{ApiName}getall");
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
using PhotoSite.WebApi.Host.IntegrationTests.Base;
using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests
{
    [Collection("Test collection")]
    public class PhotoControllerTest
    {
        internal const string ApiName = "api/photo/";

        private readonly BaseTestServerFixture _fixture;

        public PhotoControllerTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }
    }
}
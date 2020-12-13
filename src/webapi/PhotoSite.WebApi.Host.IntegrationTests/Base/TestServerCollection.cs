using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests.Base
{
    [CollectionDefinition("Test collection")]
    public class TestServerCollection : ICollectionFixture<BaseTestServerFixture>
    {
        
    }
}
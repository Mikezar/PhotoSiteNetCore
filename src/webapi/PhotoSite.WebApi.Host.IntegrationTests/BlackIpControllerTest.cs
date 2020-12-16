using System.Net;
using System.Threading.Tasks;
using PhotoSite.WebApi.Admin;
using PhotoSite.WebApi.Common;
using PhotoSite.WebApi.Host.IntegrationTests.Base;
using Xunit;

namespace PhotoSite.WebApi.Host.IntegrationTests
{
    [Collection("Test collection")]
    public class BlackIpControllerTest
    {
        private readonly BaseTestServerFixture _fixture;

        public BlackIpControllerTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        // TODO: Add tests with subnetMask = 0
        [Theory]
        [InlineData("192.168.5.85", 24, "192.168.5.1", "192.168.4.254", false)]
        //[InlineData("192.168.5.85", 24, "192.168.5.254", "191.168.5.254", false)]
        [InlineData("10.128.240.50", 30, "10.128.240.48", "10.128.240.47", false)]
        [InlineData("10.128.240.50", 30, "10.128.240.49", "10.128.240.52", false)]
        [InlineData("10.128.240.50", 30, "10.128.240.50", "10.128.239.50", false)]
        [InlineData("10.128.240.50", 30, "10.128.240.51", "10.127.240.51", false)]
        [InlineData("2001:db8:abcd:0012::0", 64, "2001:0DB8:ABCD:0012:0000:0000:0000:0000", "2001:0DB8:ABCD:0011:FFFF:FFFF:FFFF:FFFF", true)]
        [InlineData("2001:db8:abcd:0012::0", 64, "2001:0DB8:ABCD:0012:FFFF:FFFF:FFFF:FFFF", "2001:0DB8:ABCD:0013:0000:0000:0000:0000", true)]
        [InlineData("2001:db8:abcd:0012::0", 64, "2001:0DB8:ABCD:0012:0001:0000:0000:0000", "2001:0DB8:ABCD:0013:0001:0000:0000:0000", true)]
        [InlineData("2001:db8:abcd:0012::0", 64, "2001:0DB8:ABCD:0012:FFFF:FFFF:FFFF:FFF0", "2001:0DB8:ABCD:0011:FFFF:FFFF:FFFF:FFF0", true)]
        [InlineData("2001:db8:abcd:0012::0", 128, "2001:0DB8:ABCD:0012:0000:0000:0000:0000", "2001:0DB8:ABCD:0012:0000:0000:0000:0001", true)]
        public async Task ComplexTest(string maskAddress, int subnetMask, string ipAddressBlack, string ipAddressWhite, bool v6)
        {
            var adminClient = _fixture.AdminClient;
            var model = new BlackIpDto { MaskAddress = maskAddress, SubnetMask = subnetMask, IsInterNetworkV6 = v6 };
            var resultAddIp = await _fixture.PostAsync<BlackIpDto, IdResultDto>(adminClient, "api/blacklist/create", model);
            Assert.NotNull(resultAddIp);

            var client = _fixture.GetUserClient();
            client.DefaultRequestHeaders.Add(FakeRemoteIpAddressMiddleware.FakeIpAddressHeaderName, ipAddressBlack);

            // Call open metod
            var response = await client.GetAsync("/api/wm/byphoto?photoId=0");
            Assert.True(response.StatusCode == HttpStatusCode.Forbidden);

            client.DefaultRequestHeaders.Remove(FakeRemoteIpAddressMiddleware.FakeIpAddressHeaderName);
            client.DefaultRequestHeaders.Add(FakeRemoteIpAddressMiddleware.FakeIpAddressHeaderName, ipAddressWhite);

            // Call open metod
            response = await client.GetAsync("/api/wm/byphoto?photoId=0");
            Assert.True(response.StatusCode == HttpStatusCode.NoContent);

            client.DefaultRequestHeaders.Remove(FakeRemoteIpAddressMiddleware.FakeIpAddressHeaderName);

            var result = await _fixture.GetAsync<ResultDto>(adminClient, $"/api/blacklist/delete?id={resultAddIp!.Id}");
            Assert.NotNull(result);
            Assert.True(result!.ErrorMessage == null);
        }

        [Fact]
        public async Task UserUnauthorizedCreateTest()
        {
            await _fixture.UserUnauthorizedPostTest("/api/blacklist/create");
        }

        [Fact]
        public async Task UserUnauthorizedUpdateTest()
        {
            await _fixture.UserUnauthorizedPostTest("/api/blacklist/update");
        }

        [Fact]
        public async Task UserUnauthorizedDeleteTest()
        {
            await _fixture.UserUnauthorizedGetTest("/api/blacklist/delete");
        }

        [Fact]
        public async Task UserUnauthorizedGetAllTest()
        {
            await _fixture.UserUnauthorizedGetTest("/api/blacklist/getall");
        }

    }
}
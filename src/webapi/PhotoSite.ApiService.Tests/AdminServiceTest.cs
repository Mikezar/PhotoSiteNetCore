using Microsoft.Extensions.Options;
using Moq;
using PhotoSite.ApiService.Data.Admin;
using PhotoSite.ApiService.Services.Implementations;
using PhotoSite.Shared;
using Xunit;

namespace PhotoSite.ApiService.Tests
{
    public class AdminServiceTest
    {

        //[Fact] ON only for create hash for new password
        public void GenerateAdminPassword()
        {
            var loginOptions = new LoginOptions()
            {
                Login = "",
                Password = ""
            };

            var optionsSnapshot = new Mock<IOptionsSnapshot<LoginOptions>>().Apply(opMock =>
            {
                opMock.Setup(t => t.Value)
                    .Returns(loginOptions);
            }).Object;

            var service = new AdminService(optionsSnapshot);
            var hash = service.GetPasswordHash("Devel", "123456");
            Assert.NotEmpty(hash);
        }
    }
}
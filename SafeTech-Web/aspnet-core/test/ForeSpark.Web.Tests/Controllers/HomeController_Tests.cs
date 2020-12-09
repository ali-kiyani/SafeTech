using System.Threading.Tasks;
using ForeSpark.Models.TokenAuth;
using ForeSpark.Web.Controllers;
using Shouldly;
using Xunit;

namespace ForeSpark.Web.Tests.Controllers
{
    public class HomeController_Tests: ForeSparkWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
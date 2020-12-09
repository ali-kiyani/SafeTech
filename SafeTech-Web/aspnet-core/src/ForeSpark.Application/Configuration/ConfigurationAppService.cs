using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ForeSpark.Configuration.Dto;

namespace ForeSpark.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ForeSparkAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}

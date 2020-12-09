using System.Threading.Tasks;
using ForeSpark.Configuration.Dto;

namespace ForeSpark.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

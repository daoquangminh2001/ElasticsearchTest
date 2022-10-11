using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElasticsearchTest.Installer
{
    public interface IInstaller
    {
        void IInstallerServices(IServiceCollection services,IConfiguration configuration);
    }
}
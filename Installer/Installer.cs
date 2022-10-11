using ElasticsearchTest.DBContext;
using ElasticsearchTest.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElasticsearchTest.Installer
{
    public class Installer:IInstaller
    {
        public void IInstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddSingleton<WeatherContext>();
            services.AddScoped<IAuth, Auth>();
            
        }
    }
}
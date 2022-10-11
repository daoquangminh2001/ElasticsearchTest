using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
namespace ElasticsearchTest.Installer
{
    public class SystemInstaller:IInstaller
    {
        public void IInstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ElasticsearchTest", Version = "v1" });
            });
        }
    }
}
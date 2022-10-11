using ElasticsearchTest.DBContext;
using ElasticsearchTest.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
namespace ElasticsearchTest.Installer
{
    public class SystemInstaller:IInstaller
    {
        public void IInstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<WeatherContext>(); 
            services.AddControllers();
            services.AddScoped<IAuth, Auth>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ElasticsearchTest", Version = "v1" });
            });
            
        }
    }
}
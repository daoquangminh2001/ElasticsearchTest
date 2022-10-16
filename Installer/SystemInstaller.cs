using ElasticsearchTest.DBContext;
using ElasticsearchTest.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace ElasticsearchTest.Installer
{
    public class SystemInstaller:IInstaller
    {
        public void IInstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<WeatherContext>(); 
            services.AddControllers();
            services.AddScoped<IAuth, Auth>();
            services.AddSwaggerGen(options => {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value)),
             ValidateIssuer = false,
             ValidateAudience = false,
         };

     });
            services.AddAuthorization(opt => opt.AddPolicy("admin",
                authBuiler =>
                {
                    authBuiler.RequireRole("admin");
                }));
        }
    }
}
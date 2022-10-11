using ElasticsearchTest.DBContext;
using ElasticsearchTest.Input;
using ElasticsearchTest.Models;
using Microsoft.Extensions.Configuration;

namespace ElasticsearchTest.Services
{
    public class Auth:IAuth
    {
        private readonly WeatherContext _context;
        private readonly IConfiguration _configuration;

        public Auth(WeatherContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public CreateUserInput Create_Use(CreateUserInput input)
        {
            var user = new Users();
            return null;
        }
        public LoginInput Login(LoginInput input)
        {
            return null;
        }
    }
}
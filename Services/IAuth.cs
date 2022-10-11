using System.Threading.Tasks;
using ElasticsearchTest.Input;
using ElasticsearchTest.Models;

namespace ElasticsearchTest.Services
{
    public interface IAuth
    {
        public CreateUserInput Create_Use(CreateUserInput input);
        public LoginInput Login(LoginInput input);
    }
}
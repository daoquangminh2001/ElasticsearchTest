using System.Threading.Tasks;
using ElasticsearchTest.Input;
using ElasticsearchTest.Models;

namespace ElasticsearchTest.Services
{
    public interface IAuth
    {
        public dynamic GetUsers();
        public CreateUserInput Create_Use(CreateUserInput input);
        public string Login(LoginInput input);
    }
}
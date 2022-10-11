using System.Data;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dapper;
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
            string sqlQuery =
                $"INSERT INTO USERS VALUES (@User_ID,@User_Name,@age,@gender,@Password,@PasswordHash,@PasswordSalt,@Email)";
            
            using (var connect = _context.CreateConnection())
            {
                var user = new Users();
                CreatePassHash(input.Password, out byte[] passHash, out byte[] passSalt);
                user.User_ID = RandomNumberGenerator.GetInt32(1,100);
                user.User_Name = input.User_Name;
                user.Password = input.Password;
                user.PasswordHash = passHash;
                user.PasswordSalt = passSalt;
                user.Email = input.Email;
                if (connect.State == ConnectionState.Closed) connect.Open();
                 connect.Execute(sqlQuery, new
                {
                     user.User_ID,
                    input.User_Name,
                    input.age,
                    input.gender,
                    input.Password,
                    user.PasswordHash,
                    user.PasswordSalt,
                    passSalt,
                    input.Email,
                    input.role
                });
            }
            return input;
        }
        public LoginInput Login(LoginInput input)
        {
            return null;
        }
        private void CreatePassHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
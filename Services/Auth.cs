using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using Dapper;
using ElasticsearchTest.DBContext;
using ElasticsearchTest.Input;
using ElasticsearchTest.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;

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

        public dynamic GetUsers()
        {
            string Query = "Select * from dbo.Users";
            var result = new List<Users>();
            using (var connect = _context.CreateConnection())
            {
                if (connect.State == ConnectionState.Closed) connect.Open();
                var temp = connect.Query<Users>(Query);
                result = temp.ToList();
            }

            return result;
        }

        public CreateUserInput Create_Use(CreateUserInput input)
        {
            string sqlQuery =
                $"INSERT INTO dbo.USERS VALUES (@User_ID,@User_Name,@gender,@age,@Role,@Password,@PasswordHash,@PasswordSalt,@City_ID)";
            
            using (var connect = _context.CreateConnection())
            {
                var user = new Users();
                CreatePassHash(input.Password, out byte[] passHash, out byte[] passSalt);
                user.User_ID = RandomNumberGenerator.GetInt32(1,100);
                user.User_Name = input.User_Name;
                user.Password = input.Password;
                user.PasswordHash = passHash;
                user.PasswordSalt = passSalt;
                user.City_ID = null;
                if (connect.State == ConnectionState.Closed) connect.Open();
                 connect.Execute(sqlQuery, new
                {
                    user.User_ID,
                    input.User_Name,
                    input.gender,
                    input.age,
                    input.role,
                    input.Password,
                    user.PasswordHash,
                    user.PasswordSalt,
                    input.City_ID
                });
            }
            return input;
        }
        public string Login(LoginInput input)
        {
            string Token = null;
            bool check = false;
            var user = new Users();
            using (var connect = _context.CreateConnection())
            {
                if (connect.State == ConnectionState.Closed) connect.Open();
                var result = new DynamicParameters();
                result.Add("@User_Name",input.User_Name);
                result.Add("@Password",input.Password);
                var value = connect.Query<Users>("dbo.Check_User", result, commandType: CommandType.StoredProcedure);
                value.ToList();

                foreach (var a in value)
                {
                    user = a;
                }

                if (VerifiPassword(input.Password, user.PasswordSalt, user.PasswordHash)) Token = CreateToken(user);
                else return "Sai mật khẩu, ngu";
            }
            
            return Token;
        }
        private void CreatePassHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifiPassword(string p, byte[] salt, byte[] hash)
        {
            using (var temp = new HMACSHA512(salt))
            {
                var check = temp.ComputeHash(System.Text.Encoding.UTF8.GetBytes(p));
                return check.SequenceEqual(hash);
            }
        }
        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.User_Name),
                new Claim(ClaimTypes.Role, user.role==true?"admin":"user"),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(10),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
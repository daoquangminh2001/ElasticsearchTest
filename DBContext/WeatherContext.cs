using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;

namespace ElasticsearchTest.DBContext
{
    public class WeatherContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connection;

        public WeatherContext(IConfiguration configuration, string connection)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection() =>new SqlConnection(_connection);
    }
}
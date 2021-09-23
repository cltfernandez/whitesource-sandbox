using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
namespace VulnerableApp.Repositories
{
    public class UserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetNonSensitiveDataById(string id)
        {
            using (var connection = new SqlConnection(_configuration.GetValue<string>("ConnectionString")))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT * FROM NonSensitiveDataTable WHERE Id = {id}", connection);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string returnString = string.Empty;
                        returnString += $"Name : {reader["Name"]}. ";
                        returnString += $"Description : {reader["Description"]}";
                        return returnString;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        } 
    }
}
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApi.Data
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("WebConnectionString")!;
        }

        public async Task<IEnumerable<T>> GetData<T, P>(string query, P parameters)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<T>(query, parameters);
        }

        public async Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionId = "WebConnectionString")
        {
            using IDbConnection connection = new SqlConnection
                (_configuration.GetConnectionString(connectionId));
            return await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<P>(string query, P parameters)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(query, parameters);
        }
    }
}

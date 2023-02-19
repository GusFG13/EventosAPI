using Dapper;
using MySqlConnector;
using System.Data;

namespace EventosAPI.Services
{
    public class DbService : IDbService
    {
        private readonly string _db;

        public DbService()
        {
            _db = Environment.GetEnvironmentVariable("DATABASE_CONFIG"); ;
        }


         public async Task<T> GetAsync<T>(string command, object parms)
        { // query vem como parametro em command

            using MySqlConnection conn = new(_db);

            T result;
            result = (await conn.QueryAsync<T>(command, parms).ConfigureAwait(false)).FirstOrDefault();
            return result;

        }

        public async Task<List<T>> GetAll<T>(string command, object parms)
        {// query vem como parametro em command

            using MySqlConnection conn = new(_db);

            List<T> result = new List<T>();
            result = (await conn.QueryAsync<T>(command, parms)).ToList();
            return result;
        }

        public async Task<int> EditData(string command, object parms)
        {// query vem como parametro em command

            using MySqlConnection conn = new(_db);

            int result;
            result = await conn.ExecuteAsync(command, parms);
            return result;
        }
    }
}

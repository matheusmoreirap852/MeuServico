using Microsoft.Data.Sqlite;

using System.Data;

namespace BackEndApi.Context
{
    public class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public DbSession(IConfiguration configuration)
        {
            Connection = new SqliteConnection(configuration.GetConnectionString("Padrao"));

            Connection.Open();
        }
        public void Dispose() => Connection.Dispose();
    }
}

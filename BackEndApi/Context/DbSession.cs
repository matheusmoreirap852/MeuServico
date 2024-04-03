using Microsoft.Data.SqlClient;
using System.Data;

namespace BackEndApi.Context
{
    public class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public DbSession(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetConnectionString("Padrao"));
            Connection.Open();
        }
        public void Dispose() => Connection.Dispose();
    }
}

using Microsoft.Data.SqlClient;
using TDC.Backend.DataRepository.Config;

namespace TDC.Backend.DataRepository.Helper
{
    public class ConnectionFactory(ConnectionStrings connectionStrings)
    {
        private readonly string _sqlConnectionString = connectionStrings.Sql;

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(this._sqlConnectionString);
        }
    }
}

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace IpertechCompany.DbContext
{
    class DbContext : IDbContext
    {
        public string ConnectionString { get; set; }
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }

        public DbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection Connect()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
